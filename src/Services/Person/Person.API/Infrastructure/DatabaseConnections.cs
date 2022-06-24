using Autofac;
using BuildingBlocks.EventBus;
using BuildingBlocks.EventBusRabbitMQ;
using RabbitMQ.Client;

namespace Person.API.Infrastructure {
	public static class DatabaseConnections {
		public static IServiceCollection AddMySQL(this IServiceCollection services, IConfiguration configuration) {
			services.AddDbContext<PersonContext>(options => options.UseMySql(configuration.GetConnectionString("MySQL"), new MySqlServerVersion(new Version(8, 0, 26)),
					opts => {
						opts.EnableRetryOnFailure(
							maxRetryCount: 10,
							maxRetryDelay: TimeSpan.FromSeconds(10),
							errorNumbersToAdd: null
						);
					}
				),
				ServiceLifetime.Transient
			);

			RegisterEventBus(services);

			return services;
		}

		public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration) {
			services.AddSingleton<IRabbitMQPersistentConnection>(sp => {
				ILogger<DefaultRabbitMQPersistentConnection> logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

				int retryCount = 5;
				ConnectionFactory factory = new() {
					Uri = new Uri(configuration.GetConnectionString("RabbitMQ")),
					DispatchConsumersAsync = true
				};

				return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
			});

			return services;
		}

		private static void RegisterEventBus(IServiceCollection services) {
			services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => {
				int retryCount = 5;
				string subscriptionClientName = "Person";
				IRabbitMQPersistentConnection rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
				ILifetimeScope iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
				ILogger<EventBusRabbitMQ> logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
				IEventBusSubscriptionsManager eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

				return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
			});

			services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
		}
	}
}
