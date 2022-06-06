global using Person.API.Infrastructure;
global using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<PersonContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("MySQL"),
		new MySqlServerVersion(new Version(8, 0, 22)),
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
