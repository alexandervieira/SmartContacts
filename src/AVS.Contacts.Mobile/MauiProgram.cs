using AVS.Contacts.ACL;
using AVS.Contacts.Application;
using AVS.Contacts.Infrastructure;
using AVS.Contacts.Mobile.ViewModels;
using AVS.Contacts.Mobile.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AVS.Contacts.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Configuration
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("AVS.Contacts.Mobile.appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream!)
            .Build();

        builder.Configuration.AddConfiguration(config);

        // Services
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddACL(builder.Configuration);

        // ViewModels and Views
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ContactsPageViewModel>();
        builder.Services.AddTransient<ContactsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}