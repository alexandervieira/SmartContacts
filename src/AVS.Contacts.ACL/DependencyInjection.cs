using AVS.Contacts.ACL.Configuration;
using AVS.Contacts.ACL.Services;
using AVS.Contacts.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AVS.Contacts.ACL;

public static class DependencyInjection
{
    public static IServiceCollection AddACL(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SpeechSettings>(configuration.GetSection("AzureSpeech"));
        services.AddScoped<ISpeechService, AzureSpeechService>();

        return services;
    }
}