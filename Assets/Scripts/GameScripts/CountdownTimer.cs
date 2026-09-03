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

    //[SerializeField] private int breakInterval;
    //[SerializeField] private int breakDuration;

    // Read-only from other classes
    public TimeSpan TimeRemaining => timeRemaining;
    public bool IsTimerRunning => isTimerRunning;
    public bool IsTimerPaused => isTimerPaused;
    public TimeSpan TotalTimeRun => totalTimeRun;
    public float TotalMultiplier => totalMultiplier;

    // Invoked when timer stops
    public event Action OnTimerStop;


    void Update()
    {
        // Stop update while the timer is stopped or paused
        if (!isTimerRunning || isTimerPaused) return;
        
        // Decrease the remaining time until the timer reaches 0
        if(timeRemaining > TimeSpan.Zero)
        {
            timeRemaining -= TimeSpan.FromSeconds(Time.deltaTime);
            totalTimeRun += TimeSpan.FromSeconds(Time.deltaTime);
        }
        else
        {
            timeRemaining = TimeSpan.Zero;
            TimerFinished();
        }
    }

    public void StartTimer()
    {
        // Reset total time before every run
        totalTimeRun = TimeSpan.Zero;

        isTimerRunning = true;
        isTimerPaused = false;
    }

    // Stop the timer when the countdown reaches 0
    public void TimerFinished()
    {
        stopButton.onClick.Invoke();
    }

    // Stop and reset the timer
    public void StopTimer()
    {
        isTimerRunning = false;
        SetDuration(sliderTicks);
        OnTimerStop?.Invoke();
    }

    // Toggle the timer between paused and running states
    public void PauseTimer()
    {
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
}
