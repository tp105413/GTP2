using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillDescriptionText;

    public TextMeshProUGUI attackDamageValue;
    public TextMeshProUGUI attackSpeedValue;
    public TextMeshProUGUI attackIntervalValue;
    public TextMeshProUGUI criticalChanceValue;
    public TextMeshProUGUI criticalDamageValue;


    void Awake()
    {
        // Refresh stats value once after the game is started
        RefreshStatsValue();
    }

    private void OnEnable()
    {
        // Subscribe to stats value change events
        playerStats.OnStatsValueChanged += RefreshStatsValue;
    }

    // Unsubscribe all events when GameObject is disable
    public void OnDisable()
    {
        if(playerStats != null)
        {
            playerStats.OnStatsValueChanged -= RefreshStatsValue;
        }
    }

    // Display the skill name, description and level
    public void ShowSkill(string skillName, string skillDescription, int level, int dmg, float spd, float interval, int crit)
    {
        skillNameText.text = $"{skillName} Lv.{level}";
        skillDescriptionText.text = skillDescription
            .Replace("{dmg}", dmg.ToString())
            .Replace("{spd}", spd.ToString("F2"))
            .Replace("{interval}", interval.ToString("F2"))
            .Replace("{crit}", crit.ToString());
    }

    // Update player stats UI display
    public void RefreshStatsValue()
    {
        attackDamageValue.text = $"{playerStats.AttackDamage}";
        attackSpeedValue.text = $"{playerStats.AttackSpeed}";
        attackIntervalValue.text = $"{playerStats.AttackInterval:F2}s";
        criticalChanceValue.text = $"{playerStats.CriticalChance}%";
        criticalDamageValue.text = $"{playerStats.CriticalDamage}%";
    }
}
