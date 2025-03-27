using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Behaviors;
using Dhanman.MyHome.Application.Caching;
using Dhanman.MyHome.Application.Extentions;
using Dhanman.MyHome.Application.ServiceClient;
using Dhanman.MyHome.Application.Services;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dhanman.MyHome.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<CacheService>();
        services.AddHttpClient<ICommonServiceClient, CommonServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:CommonServiceBaseAddress"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        services.AddHttpClient<ISalesServiceClient, SalesServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:SalesServiceBaseAddress"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        services.AddHttpClient<IPurchaseServiceClient, PurchaseServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:PurchaseServiceBaseAddress"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        var firebaseKeyPath = Path.Combine(AppContext.BaseDirectory, "serviceAccountKey.json");

        services.AddHttpClient<IFirebaseService, FirebaseNotificationService>();
        services.AddSingleton(new FirebaseAuth(firebaseKeyPath));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            //config.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            config.NotificationPublisher = new TaskWhenAllPublisher();
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));

        return services;
    }
}
