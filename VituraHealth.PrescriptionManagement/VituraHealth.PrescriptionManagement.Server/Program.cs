using VituraHealth.PrescriptionManagement.Server.Interfaces;
using VituraHealth.PrescriptionManagement.Server.Service;
using VituraHealth.PrescriptionManagement.Infrastructure;
using VituraHealth.PrescriptionManagement.Infrastructure.Settings;


var builder = WebApplication.CreateBuilder(args);

/// Below we will:
/// 1. Get the configuration from appsettings.json
/// 2. Register the JsonDataStoreConfig as a singleton service, so it can be DI loaded by the JsonPrescriptionDataAccessor
/// 3. Register the JsonPrescriptionDataAccessor as a singleton service, so it can be DI loaded by the PrescriptionService
/// 4. Register the PrescriptionService as a singleton service, so it can be DI loaded by the controllers

//1. Get the configuration from appsettings.json
var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

// Load the JsonDataStoreConfig from the configuration file
var section = config.GetSection("JsonDataStore");
var prescriptionDataConfig = section.Get<JsonDataStoreConfig>();

// 2. Register the JsonDataStoreConfig as a singleton service, so it can be DI loaded by the json data accessor
if (prescriptionDataConfig != null)
    builder.Services.AddSingleton(prescriptionDataConfig);
else
    throw new InvalidOperationException("Failed to load JsonDataStoreConfig from configuration. Please update appsettings to fix this error.");

// 3. Register the JsonPrescriptionDataAccessor as a singleton service, so it can be DI loaded by the prescription service
builder.Services.UseJsonInMemoryPrescriptionData();

// 4. Register the PrescriptionService as a singleton service, so it can be DI loaded by the controllers
builder.Services.AddSingleton<IPrescriptionService, PrescriptionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5300")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Swagger services to the DI container
builder.Services.AddSwaggerGen();

// Build the WebApp
var app = builder.Build();

app.UseCors("AllowLocalhost");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
