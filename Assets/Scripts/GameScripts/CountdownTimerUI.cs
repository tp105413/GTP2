using UnityEngine;
using TMPro;

public class CountdownTimerUI : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        if (timerText == null) return;
        timerText.text = countdownTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
    }

    public void OnSliderValueChanged(float value)
    {
        countdownTimer.SliderTicks = value;
        Debug.Log("The slider value is now: " + value);
    }
}
