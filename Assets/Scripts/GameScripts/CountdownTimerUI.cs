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
        if (timerText == null) return;
        timerText.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
        timerText2.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
    }

    public void OnSliderValueChanged(float value)
    {
        countdownTimer.SetDuration(value);
        Debug.Log("The slider value is now: " + value);
    }
}
