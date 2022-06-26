using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
	var origins = builder.Configuration.GetSection("CorsOrigin").Get<string[]>();
	options.AddDefaultPolicy(build => {
		build.SetIsOriginAllowedToAllowWildcardSubdomains()
			.WithOrigins(origins)
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials();
	});
});

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"Configurations/ocelot.{builder.Environment.EnvironmentName}.json").Build();
builder.Services.AddOcelot(configuration).AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
