using UnityEngine;
using TMPro;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerText2;


    void Update()
    {
        // Update the timer display
        if (timerText == null) return;
        timerText.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
        timerText2.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
    }

    // Update the timer duration when slider value changes
    public void OnSliderValueChanged(float value)
    {
        countdownTimer.SetDuration(value);
    }
}
