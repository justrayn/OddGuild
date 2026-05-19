using Microsoft.Extensions.Logging;
using OddGuild.Views;
using OddGuild.ViewModels;

namespace OddGuild;

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

                fonts.AddFont("PixelifySans-Regular.ttf", "PixelifyRegular");
                fonts.AddFont("PixelifySans-Bold.ttf", "PixelifyBold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register your ViewModels (The Brains)
        // Removed MainViewModel, kept QuestViewModel
        builder.Services.AddTransient<QuestViewModel>();

        // Register your Views (The Faces)
        // Removed MainPage, added all your new custom pages!
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<QuestPage>();
        builder.Services.AddTransient<PartyPage>();
        builder.Services.AddTransient<ProfilePage>();
        
        return builder.Build();
    }
}