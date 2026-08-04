using System;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Settings")]
    [SerializeField] public float SliderTicks;
    [SerializeField] private float totalDurationInSeconds;

    private TimeSpan timeRemaining;
    private bool isTimerRunning = false;
    private bool isTimerPaused = false;

    public event Action OnTimerStart;
    public event Action OnTimerFinished;

    void Start()
    {

    }

    private void StartTimer() 
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

    private void UpdateTimerDisplay()
    {
        if (timerText == null) return;
        timerText.text = timeRemaining.ToString(@"hh\:mm\:ss");
    }

    private void TimerFinished()
    {
        isTimerRunning = false;
        Debug.Log("Countdown complete!");
        OnTimerFinished?.Invoke(); 
    }
    private void StopTimer()
    {
        isTimerRunning = false;
        OnTimerFinished?.Invoke();
    }
    public void OnSliderValueChanged(float value)
    {   
        SliderTicks = value;
        Debug.Log("The slider value is now: " + value);      
    }

    public void OnStartButtonClick()
    {   
        StartTimer();      
    }

    public void OnStopButtonClick()
    {
        StopTimer();
    }

    public void OnPauseButtonClick()
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
