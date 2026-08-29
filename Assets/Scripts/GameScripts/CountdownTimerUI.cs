using UnityEngine;
using TMPro;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerText2;
    [SerializeField] private TextMeshProUGUI multiplierText;


    void Update()
    {
        // Update the timer and reward multiplier display
        if (timerText == null) return;
        timerText.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
        timerText2.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
        multiplierText.text = "x" + countdownTimer.TotalMultiplier.ToString();
    }

    // Update the timer duration when slider value changes
    public void OnSliderValueChanged(float value)
    {
        countdownTimer.SetDuration(value);
    }
}
