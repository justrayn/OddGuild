namespace OddGuild;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // This tells the app to load your custom tabs!
        return new Window(new AppShell());
    }
}