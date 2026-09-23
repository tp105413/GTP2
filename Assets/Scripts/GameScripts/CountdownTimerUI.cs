using UnityEngine;
using TMPro;
using System;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerText2;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI breakTimeLabel;


    void Update()
    {
        if (!countdownTimer.IsOnBreak)
        {
            // Update the timer and reward multiplier display
            if (timerText == null) return;
            timerText.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
            timerText2.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
            multiplierText.text = "x" + countdownTimer.TotalMultiplier.ToString("0.##");

            breakTimeLabel.gameObject.SetActive(false);
        }
        else
        {
            // Update the break timer text
            TimeSpan remaining = TimeSpan.FromSeconds(countdownTimer.BreakTimeRemaining);
            timerText2.text = remaining.ToString(@"hh\:mm\:ss");

            breakTimeLabel.gameObject.SetActive(true);
        }
    }

    // Update the timer duration when slider value changes
    public void OnSliderValueChanged(float value)
    {
        countdownTimer.SetDuration(value);
    }
}
