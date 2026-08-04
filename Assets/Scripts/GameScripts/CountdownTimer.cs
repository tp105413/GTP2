using System;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float SliderTicks;
    [SerializeField] private float totalDurationInSeconds;

    private TimeSpan timeRemaining;
    private bool isTimerRunning = false;
    private bool isTimerPaused = false;

    // read only
    public TimeSpan TimeRemaining => timeRemaining;

    public event Action OnTimerStart;
    public event Action OnTimerFinished;

    public void StartTimer()
    {
        isTimerRunning = true;
        isTimerPaused = false;
        OnTimerStart?.Invoke();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isTimerRunning) {
            totalDurationInSeconds = 1800 * SliderTicks + 1;
            timeRemaining = TimeSpan.FromSeconds(totalDurationInSeconds);
            UpdateTimerDisplay();
        }

        if (timeRemaining.TotalSeconds > 0 && !isTimerPaused)
        {  
            timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
            UpdateTimerDisplay();      
        }
        else if (timeRemaining.TotalSeconds <= 0)
        {
            timeRemaining = TimeSpan.Zero;
            isTimerPaused = true;
            UpdateTimerDisplay();
            TimerFinished();
        }
    }

    public void UpdateTimerDisplay()
    {

    }

    public void TimerFinished()
    {
        isTimerRunning = false;
        Debug.Log("Countdown complete!");
        OnTimerFinished?.Invoke(); 
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        OnTimerFinished?.Invoke();
    }

    public void PauseTimer()
    {
        if (!isTimerPaused) 
        {
            isTimerPaused = true;
            Debug.Log("Timer paused!");
        }
        else if (isTimerPaused)
        {
            isTimerPaused = false;
            Debug.Log("Timer resumed!");
        }
    }
}
