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
	private void StartAndPause(object sender, EventArgs e)
	{
		if (!_isRunning)
		{
			_isRunning = true;

			StartAndPauseTimer.Text = "Pause";

			_timer = new System.Timers.Timer(1000);
			_timer.Elapsed += UpdateTimer;
			_timer.Start();
		}
		else
		{
			_timer?.Stop();
			_isRunning = false;

			StartAndPauseTimer.Text = "Start";
		}
	}

	private void ResetTimer(object sender, EventArgs e)
	{
		_timer?.Stop();
		_remainingTime = 25 * 60;
		_isRunning = false;

		StartAndPauseTimer.Text = "Start";

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

				_remainingTime = 25 * 60;
				UpdateTimerLabel();

				StartAndPauseTimer.Text = "Start";
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

