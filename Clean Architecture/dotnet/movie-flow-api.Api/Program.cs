using System.Net;
using System.Reflection;
using Microsoft.Extensions.Options;
using movie_flow_api.Api.Swagger;
using movie_flow_api.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;
using movie_flow_api.Application;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Déterminer l'environnement actuel
var environment = builder.Environment.EnvironmentName;

// Charger le fichier .env approprié en fonction de l'environnement
var envFilePath = environment switch
{
    "Development" => "local.env",
    "Production" => "production.env",
    _ => "local.env"
};

Console.WriteLine($"Loading environment variables from {envFilePath}");

Env.Load(envFilePath);

builder.Configuration["ConnectionStrings:PGSQL"] = Environment.GetEnvironmentVariable("DATABASE_URL");


// Add services to the container.
builder.Services.AddControllers();

// Add Versioning configuration
builder.Services
    .AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Swagger generator
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        // Build a swagger endpoint for each discovered API Version
        foreach (var groupName in descriptions.Select(x => x.GroupName))
        {
            var url = $"/swagger/{groupName}/swagger.json";
            var name = groupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
