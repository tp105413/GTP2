using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance;

    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private TMP_InputField breakInterval;
    [SerializeField] private TMP_InputField breakDuration;
    [SerializeField] private TMP_Dropdown breakIntervalDropDown;
    [SerializeField] private TMP_Dropdown breakDurationDropDown;
    [SerializeField] private Button OnOffButton;

    public Image toggleImage;
    public Sprite onImage;
    public Sprite offImage;

    // Update the break interval based on the input and selected time unit
    public void OnBreakIntervalChanged(string value)
    {
        int interval = 0;

        // Check whether value is empty
        if (!string.IsNullOrEmpty(value))
        {
            interval = int.Parse(value);
        }

        int dropDownOption = breakIntervalDropDown.value;

        switch (dropDownOption)
        {
            case 0:
                interval *= 3600;
                break;
            case 1:
                interval *= 60;
                break;
            default:
                break;
        }
        countdownTimer.SetBreakInterval(interval);
    }

    // Update the break duration based on the input and selected time unit
    public void OnBreakDurationChanged(string value)
    {
        int duration = 0;

        // Check whether value is empty
        if (!string.IsNullOrEmpty(value))
        {
            duration = int.Parse(value);
        }

        int dropDownOption = breakDurationDropDown.value;

        switch (dropDownOption)
        {
            case 0:
                duration *= 3600;
                break;
            case 1:
                duration *= 60;
                break;
            default:
                break;
        }

        countdownTimer.SetBreakDuration(duration);
    }

    // Update the break interval when time unit dropdown changes
    public void OnBreakIntervalDropdownChanged(int value)
    {
        OnBreakIntervalChanged(breakInterval.text);
    }

    // Update the break duration when time unit dropdown changes
    public void OnBreakDurationDropdownChanged(int value)
    {
        OnBreakDurationChanged(breakDuration.text);
    }

    // Toggle break mode button function
    public void BreakModeOnClicked()
    {
        if (countdownTimer.breakMode)
        {
            countdownTimer.breakMode = false;
            toggleImage.sprite = offImage;
        }
        else
        {
            countdownTimer.breakMode = true;
            toggleImage.sprite = onImage;
        }
    }
}
