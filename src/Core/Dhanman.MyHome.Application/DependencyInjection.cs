using Dhanman.MyHome.Application.Behaviors;
using Dhanman.MyHome.Application.Extentions;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dhanman.MyHome.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
