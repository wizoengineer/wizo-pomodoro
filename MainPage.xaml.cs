namespace wizo_pomodoro;

using System.Timers;

public partial class MainPage : ContentPage
{
	private System.Timers.Timer? _timer;
	private int _remainingTime = 25 * 60;
	private bool _isRunning = false;
	private int _sessionCount = 0;
	private bool _isBreak = false;

	public MainPage()
	{
		InitializeComponent();
		UpdateTimerLabel();
		UpdateSessionTypeLabel();
	}

	private void StartAndPause(object sender, EventArgs e)
	{
		if (_sessionCount == 4 && !_isRunning && !_isBreak)
		{
			_sessionCount = 0;
			ResetSessions();
		}

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
		_sessionCount = 0;
		_isBreak = false;

		ResetSessions();

		StartAndPauseTimer.Text = "Start";
		UpdateTimerLabel();
		UpdateSessionTypeLabel();
	}

	private void UpdateTimer(object sender, ElapsedEventArgs e)
	{
		if (_remainingTime <= 0)
		{
			_timer?.Stop();
			_isRunning = false;

			MainThread.BeginInvokeOnMainThread(() =>
			{
				if (_isBreak)
				{
					DisplayAlert("Break Over!", "Get ready for your next focus session", "OK");
					_isBreak = false;
					_remainingTime = 25 * 60;
				}
				else
				{
					DisplayAlert("Time's Up!", "Take a short break!", "OK");
					_sessionCount++;
					UpdateSessionColors();
					_isBreak = true;
					_remainingTime = 5 * 60;
				}

				UpdateTimerLabel();
				UpdateSessionTypeLabel();
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

	private void UpdateSessionTypeLabel()
	{
		SessionTypeLabel.Text = _isBreak ? "Break" : "Focus";
	}

	private void UpdateSessionColors()
	{
		if (_sessionCount >= 1) Session1.BackgroundColor = Color.FromArgb("#FF6347");
		if (_sessionCount >= 2) Session2.BackgroundColor = Color.FromArgb("#FF6347");
		if (_sessionCount >= 3) Session3.BackgroundColor = Color.FromArgb("#FF6347");
		if (_sessionCount >= 4) Session4.BackgroundColor = Color.FromArgb("#FF6347");
	}

	private void ResetSessions()
	{
		Session1.BackgroundColor = Colors.Gray;
		Session2.BackgroundColor = Colors.Gray;
		Session3.BackgroundColor = Colors.Gray;
		Session4.BackgroundColor = Colors.Gray;
	}
}