namespace wizo_pomodoro;

using System.Timers;

public partial class MainPage : ContentPage
{
	private System.Timers.Timer? _timer;
	private int _remainingTime = 25 * 60;
	private bool _isRunning = false;

	public MainPage()
	{
		InitializeComponent();
		UpdateTimerLabel();
	}

	private void StartTimer(object sender, EventArgs e)
	{
		if (_isRunning) return;

		_isRunning = true;

		_timer = new System.Timers.Timer(1000);
		_timer.Elapsed += UpdateTimer;
		_timer.Start();
	}

	private void PauseTimer(object sender, EventArgs e)
	{
		_timer?.Stop();
		_isRunning = false;
	}

	private void ResetTimer(object sender, EventArgs e)
	{
		_timer?.Stop();asdasdsa
		_remainingTime = 25 * 60;
		_isRunning = false;
		UpdateTimerLabel();
	}

	private void UpdateTimer(object sender, ElapsedEventArgs e)
	{
		if (_remainingTime <= 0)
		{
			_timer?.Stop();
			_isRunning = false;

			MainThread.BeginInvokeOnMainThread(() =>
			{
				DisplayAlert("Time's Up!", "Take a short break!", "OK");
			});

			return;
		}

		_remainingTime--;

		MainThread.BeginInvokeOnMainThread(UpdateTimerLabel);
	}

	private void UpdateTimerLabel()
	{
		TimerLabel.Text = $"{_remainingTime / 60:D2}:{_remainingTime % 60:D2}";
	}
}

