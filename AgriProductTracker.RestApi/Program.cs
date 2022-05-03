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

app.UseAuthorization();

app.MapControllers();

app.Run();


//public static IMvcBuilder AddControllers(this IServiceCollection services, Action<MvcOptions>? configure)

public static class CustomeExtenstionMethod
{
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
