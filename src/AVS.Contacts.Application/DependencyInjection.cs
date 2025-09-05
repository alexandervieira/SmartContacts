using AVS.Contacts.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AVS.Contacts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(typeof(ContactProfile));

        return services;
    }
}