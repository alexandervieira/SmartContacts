using AVS.Contacts.Domain.Repositories;
using AVS.Contacts.Infrastructure.Authentication;
using AVS.Contacts.Infrastructure.Mongo.Configuration;
using AVS.Contacts.Infrastructure.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AVS.Contacts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
        services.Configure<AuthSettings>(configuration.GetSection("AzureAdB2C"));

        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }
}