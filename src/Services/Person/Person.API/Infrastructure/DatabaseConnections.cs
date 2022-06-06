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

			return services;
		}
	}
}
