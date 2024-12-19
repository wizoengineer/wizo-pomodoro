namespace wizo_pomodoro;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window(new AppShell());

#if MACCATALYST || WINDOWS
		window.Width = 400;
		window.Height = 600;
		window.MinimumWidth = 400;
		window.MinimumHeight = 600;
		window.MaximumWidth = 400;
		window.MaximumHeight = 600;
#endif

		return window;
	}
}