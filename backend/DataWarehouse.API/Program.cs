using Microsoft.OpenApi.Models;
using DataWarehouse.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DataWarehouse API",
        Version = "v1"
    });
});

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataWarehouse API v1");
    c.RoutePrefix = string.Empty;
});

// ✅ CORS MUST be before endpoints
app.UseCors("AllowFrontend");

// ❌ REMOVE THIS (VERY IMPORTANT)
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

