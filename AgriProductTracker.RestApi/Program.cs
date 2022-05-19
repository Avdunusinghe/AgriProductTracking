using AgriProductTracker.Business;
using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.RestApi.Infrastructure;
using AgriProductTracker.RestApi.Infrastructure.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCustomeDbContext(builder.Configuration);
builder.Services.EnableCors(builder.Configuration);
builder.Services.EnableMultiPartBody(builder.Configuration);
builder.Services.AddSwagger();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new ApplicationModule());
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



public static class CustomeExtenstionMethod
{
    public static IServiceCollection AddCustomeDbContext(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddEntityFrameworkSqlServer().AddDbContext<AgriProductTrackerDbContext>(options =>
        {
            options.UseLazyLoadingProxies()
            .UseSqlServer(configuration["AgriProductTrackerDbConnectionString"],
                          sqlServerOptionsAction: sqlOptions =>
                          {
                              sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                          });
        });

        return service;
    }
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AgriProductTracking. - Web API",
                Version = "v1",
                Description = "The web service for SE3020 -Distributed Systems Assignment 2 Rest Api",
                TermsOfService = new Uri("https://example.com/terms")
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
          });
        });

        return services;

    }
    public static IServiceCollection EnableCors(this IServiceCollection services, IConfiguration configuration)
    {

        var allowedOrigins = new List<string>();
        var allowOrigins = configuration["AllowedOrigins"].Split(",");

        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy",
                      builder => builder.WithOrigins(allowOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
        });

        return services;
    }

    public static IServiceCollection EnableMultiPartBody(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FormOptions>(o =>
        {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = long.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
            o.ValueCountLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });

        services.AddMvc(options =>
        {
            options.MaxModelBindingCollectionSize = int.MaxValue;
        });

        return services;
    }
}

    

