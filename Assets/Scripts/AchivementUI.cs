using UnityEngine;
using TMPro;    
public class AchievementUI : MonoBehaviour
{
    [Header("TextFile")]
    [SerializeField] private TMP_Text highestLevelText;
    [SerializeField] private TMP_Text timerSessionDoneText;
    [SerializeField] private TMP_Text totalTimeText;
    [SerializeField] private TMP_Text monsterKillText;
    [SerializeField] private TMP_Text bossKillText;
    [SerializeField] private TMP_Text highestDamageText;
    [SerializeField] private TMP_Text totalGoldEarnedText;
    [SerializeField] private TMP_Text totalSoulEarnedText;
    [SerializeField] private TMP_Text ButterflyClickText;



    private void OnEnable()
    {
        RefreshRecordPanel();
    }

    public void RefreshRecordPanel()
    {
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 0);
        int timerDone = PlayerPrefs.GetInt("TimerSessionDone", 0);

        float totalTime = PlayerPrefs.GetFloat("TotalTime", 0f);
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(totalTime);
        string formattedTime = timeSpan.TotalDays >= 1
            ? string.Format("{0}d {1:D2}:{2:D2}:{3:D2}", (int)timeSpan.TotalDays, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds)
            : timeSpan.ToString(@"hh\:mm\:ss");

        int monsterKill = PlayerPrefs.GetInt("MonsterKill", 0);
        int bossKill = PlayerPrefs.GetInt("BossKill", 0);
        int highestDamage = PlayerPrefs.GetInt("HighestAttackDamage", 0);
        int goldEarned = PlayerPrefs.GetInt("TotalGoldEarned", 0);
        int soulEarned = PlayerPrefs.GetInt("TotalSoulEarned", 0);
        int ButterflyClick = PlayerPrefs.GetInt("Butterfly Click", 0);

        highestLevelText.text = $"Player Highest Level : {highestLevel}";
        timerSessionDoneText.text = $"Timer Session Done : {timerDone}";
        totalTimeText.text = $"Total Time : {formattedTime}";
        monsterKillText.text = $"Monster Kill : {monsterKill}";
        bossKillText.text = $"Boss Kill : {bossKill}";
        highestDamageText.text = $"Highest Attack Damage : {highestDamage}";
        totalGoldEarnedText.text = $"Total Gold Earned : {goldEarned}";
        totalSoulEarnedText.text = $"Total Soul Earned : {soulEarned}";
        ButterflyClickText.text = $"Butterfly Click : {ButterflyClick}";

    }
}
