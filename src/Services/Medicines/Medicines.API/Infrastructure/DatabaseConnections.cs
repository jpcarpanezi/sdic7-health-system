using Autofac;
using BuildingBlocks.EventBus;
using BuildingBlocks.EventBusRabbitMQ;
using Medicines.API.IntegrationEvents.EventHandling;
using RabbitMQ.Client;

namespace Medicines.API.Infrastructure {
	public static class DatabaseConnections {
		public static IServiceCollection AddMySQL(this IServiceCollection services, IConfiguration configuration) {
			services.AddDbContext<MedicinesContext>(options => options.UseMySql(configuration.GetConnectionString("MySQL"), new MySqlServerVersion(new Version(8, 0, 26)),
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

			RegisterEventBus(services);

			return services;
		}

		private static void RegisterEventBus(IServiceCollection services) {
			services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => {
				int retryCount = 5;
				string subscriptionClientName = "Medicines";
				IRabbitMQPersistentConnection rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
				ILifetimeScope iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
				ILogger<EventBusRabbitMQ> logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
				IEventBusSubscriptionsManager eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

				return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
			});

			services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

			services.AddTransient<DeletedPersonEventHandler>();
		}

		public static IApplicationBuilder ConfigureEventBus(this IApplicationBuilder app) {
			var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

			eventBus.Subscribe<DeletedPersonEvent, DeletedPersonEventHandler>();

			return app;
		}
	}
}
