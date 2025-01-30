using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MovieReviews.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR((conf) => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
