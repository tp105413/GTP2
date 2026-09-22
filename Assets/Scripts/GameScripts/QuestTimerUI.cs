using UnityEngine;
using TMPro;

public class QuestTimerUI : MonoBehaviour
{
    [SerializeField] private CountdownTimer questTimer;
    public QuestSystemMenu questManager;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI multiplierText;


    void Update()
    {
        // Update the timer and reward multiplier display
        if (timerText == null) return;
        timerText.text = questTimer.TimeRemaining.ToString(@"hh\:mm\:ss");
        multiplierText.text = "x" + questTimer.TotalMultiplier.ToString();
    }

    // Update the timer duration when slider value changes
    public void OnQuestSliderValueChanged(float value)
    {   
        questTimer.SetDuration(value);

        if (questManager.EditingQuest1 == true) {
            questManager.QuestTime1inTicks = value;   
        }
        if (questManager.EditingQuest2 == true) {
            questManager.QuestTime2inTicks = value;
        }
        if (questManager.EditingQuest3 == true) {
            questManager.QuestTime3inTicks = value;
        }
    }
}
