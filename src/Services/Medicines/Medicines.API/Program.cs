global using Medicines.API.Infrastructure;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using BuildingBlocks.EventBus.Events;
global using BuildingBlocks.EventBus.Abstractions;
global using Medicines.API.IntegrationEvents.Events;
global using System.ComponentModel.DataAnnotations;
global using Medicines.API.Models.Inserts;
global using Medicines.API.Models.Entities;
global using Medicines.API.Models.Queries;
global using Medicines.API.Models.Views;
global using Medicines.API.Models.Updates;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddMySQL(builder.Configuration).AddEventBus(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.ConfigureEventBus();
app.Run();