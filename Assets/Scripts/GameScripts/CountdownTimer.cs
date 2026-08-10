using System;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float sliderTicks;
    [SerializeField] private float totalDurationInSeconds;

    private TimeSpan timeRemaining;
    private bool isTimerRunning = false;
    private bool isTimerPaused = false;

    // read only
    public TimeSpan TimeRemaining => timeRemaining;

    public event Action OnTimerStop;

    public void StartTimer()
    {
        if(timeRemaining == TimeSpan.Zero)
        {
            SetDuration(sliderTicks);
        }

        isTimerRunning = true;
        isTimerPaused = false;
    }

    void Update()
    {
        if (!isTimerRunning || isTimerPaused) return;
        
        if(timeRemaining > TimeSpan.Zero)
        {
            timeRemaining -= TimeSpan.FromSeconds(Time.deltaTime);
        }
        else
        {
            timeRemaining = TimeSpan.Zero;
            TimerFinished();
        }
    }

    public void TimerFinished()
    {
        isTimerRunning = false;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        SetDuration(sliderTicks);
        OnTimerStop?.Invoke();
    }

    public void PauseTimer()
    {
        if (!isTimerPaused) 
        {
            isTimerPaused = true;
        }
        else if (isTimerPaused)
        {
            isTimerPaused = false;
        }
    }

    public void SetDuration(float ticks)
    {
        if (isTimerRunning) return;

        sliderTicks = ticks;
        totalDurationInSeconds = 1800f * ticks;
        timeRemaining = TimeSpan.FromSeconds(totalDurationInSeconds);
    }
}
