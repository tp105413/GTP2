using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float sliderTicks;
    [SerializeField] private float totalDurationInSeconds;
    [SerializeField] private Button stopButton;

    private TimeSpan timeRemaining;
    private bool isTimerRunning = false;
    private bool isTimerPaused = false;
    private TimeSpan totalTimeRun;
    private float totalMultiplier;
    private int breakInterval;
    private int breakDuration;
    public static CountdownTimer Instance { get; private set; }

    // Read-only from other classes
    public TimeSpan TimeRemaining => timeRemaining;
    public bool IsTimerRunning => isTimerRunning;
    public bool IsTimerPaused => isTimerPaused;
    public TimeSpan TotalTimeRun => totalTimeRun;
    public float TotalMultiplier => totalMultiplier;
    public int BreakInterval => breakInterval;
    public int BreakDuration => breakDuration;

    private bool isOnBreak = false;
    private float breakTimeRemaining;
    private TimeSpan breakTimer;

    public bool IsOnBreak => isOnBreak;
    public float BreakTimeRemaining => breakTimeRemaining;
    public TimeSpan BreakTimer => breakTimer;

    public bool breakMode = false;

    // Invoked when timer stops
    public event Action OnTimerStop;


    void Update()
    {
        // If break timer start, pause the game timer
        if (isOnBreak)
        {
            breakTimeRemaining -= Time.deltaTime;

            if (breakTimeRemaining <= 0)
            {
                breakTimeRemaining = 0f;
                EndBreak();
            }
            return;
        }

        // Stop update while the timer is stopped or paused
        if (!isTimerRunning || isTimerPaused) return;

        // Decrease the remaining time until the timer reaches 0
        if(timeRemaining > TimeSpan.Zero)
        {
            timeRemaining -= TimeSpan.FromSeconds(Time.deltaTime);
            totalTimeRun += TimeSpan.FromSeconds(Time.deltaTime);
            breakTimer += TimeSpan.FromSeconds(Time.deltaTime);

            // Run when break mode is on
            if (breakMode)
            {
                if (breakTimer.TotalSeconds >= breakInterval)
                {
                    StartBreak();
                }
            }
        }
        else
        {
            timeRemaining = TimeSpan.Zero;
            TimerFinished();
        }
    }

    public void StartTimer()
    {
        // Reset total time and before every run
        totalTimeRun = TimeSpan.Zero;

        isTimerRunning = true;
        isTimerPaused = false;
    }

    // Stop the timer when the countdown reaches 0
    public void TimerFinished()
    {
        stopButton.onClick.Invoke();
        int timerDone = PlayerPrefs.GetInt("TimerSessionDone", 0);
        timerDone++;
        PlayerPrefs.SetInt("TimerSessionDone", timerDone);
    }

    // Stop and reset the timer
    public void StopTimer()
    {
        isTimerRunning = false;

        // Reset break timer while stopping the timer during break
        if (isOnBreak)
        {
            isOnBreak = false;
            breakTimer = TimeSpan.Zero;
            MiniGame.Instance.StopMiniGame();
        }

        breakTimeRemaining = 0f;

        SetDuration(sliderTicks);
        OnTimerStop?.Invoke();
    }

    // Toggle the timer between paused and running states
    public void PauseTimer()
    {
        // Prevent manual pause stop the break state
        if (isOnBreak) return;

        if (!isTimerPaused) 
        {
            isTimerPaused = true;
            isTimerRunning = false;
            OnTimerStop?.Invoke();
        }
        else if (isTimerPaused)
        {
            isTimerPaused = false;
            isTimerRunning = true;
        }
    }

    // Set the timer duration if it is not running
    public void SetDuration(float ticks)
    {
        if (isTimerRunning) return;

        sliderTicks = ticks;
        totalDurationInSeconds = 10f * ticks;
        totalMultiplier = 1f + 0.25f * (totalDurationInSeconds / 10f);
        timeRemaining = TimeSpan.FromSeconds(totalDurationInSeconds);
    }

    // Set the break interval value
    public void SetBreakInterval(int value)
    {
        breakInterval = value;
    }

    // Set the break duration value
    public void SetBreakDuration(int value)
    {
        breakDuration = value;
    }

    // Pause the game and start the break countdown
    public void StartBreak()
    {
        GameManager.Instance.PauseGame(true);
        isOnBreak = true;
        breakTimeRemaining = breakDuration;

        MiniGame.Instance.StartMiniGame();
    }

    // Resume the game and reset the break countdown
    public void EndBreak()
    {
        isOnBreak = false;

        // Reset break timer
        breakTimer = TimeSpan.Zero;

        GameManager.Instance.PauseGame(false);
        MiniGame.Instance.StopMiniGame();
    }
}
