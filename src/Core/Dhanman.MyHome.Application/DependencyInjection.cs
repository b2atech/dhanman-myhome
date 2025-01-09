using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Behaviors;
using Dhanman.MyHome.Application.Caching;
using Dhanman.MyHome.Application.Extentions;
using Dhanman.MyHome.Application.ServiceClient;
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
