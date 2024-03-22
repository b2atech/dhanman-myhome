using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dhanman.MyHome.Application.Extentions;

public static class ValidatorExtention
{
    public static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Transient, Func<AssemblyScanner.AssemblyScanResult, bool> filter = null)
    {
        AssemblyScanner.FindValidatorsInAssembly(assembly).ForEach(delegate (AssemblyScanner.AssemblyScanResult scanResult)
        {
            services.AddScanResult(scanResult, lifetime, filter);
        });
        return services;
    }
    private static IServiceCollection AddScanResult(this IServiceCollection services, AssemblyScanner.AssemblyScanResult scanResult, ServiceLifetime lifetime, Func<AssemblyScanner.AssemblyScanResult, bool> filter)
    {
        if (filter?.Invoke(scanResult) ?? true)
        {
            services.Add(new ServiceDescriptor(scanResult.InterfaceType, scanResult.ValidatorType, lifetime));
            services.Add(new ServiceDescriptor(scanResult.ValidatorType, scanResult.ValidatorType, lifetime));
        }

        return services;
    }
}
