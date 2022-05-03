using AgriProductTracker.RestApi.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.EnableCors(builder.Configuration);
builder.Services.EnableMultiPartBody(builder.Configuration);
builder.Services.AddSwagger();

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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgriProductTracker.RestApi v1"));
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


//public static IMvcBuilder AddControllers(this IServiceCollection services, Action<MvcOptions>? configure)

public static class CustomeExtenstionMethod
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Agri Management. - Web API",
                Version = "v1",
                Description = "The web service for Sliit",
                TermsOfService = new Uri("https://example.com/terms")
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                //BearerFormat = "JWT",
                //Scheme = "Bearer"
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
    public static IServiceCollection EnableCors( this IServiceCollection services, IConfiguration configuration)
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
