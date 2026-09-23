using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text skillDescriptionText;
    [SerializeField] private TMP_Text upgradeCostText;
    [SerializeField] private Image costIcon;
    [SerializeField] private Sprite goldIcon;
    [SerializeField] private Sprite soulIcon;

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

    // Display the skill name, description, level and upgrade cost
    public void ShowSkill(string skillName, string skillDescription, bool isSkill, int level, int cost, int dmg, float spd, float interval,
        int crit,int sLevel, int sCost, int sDmg, int dmgMul)
    {
        string levelText;
        string sLevelText;

        // Show Lv.Max when stats and skill level reach max level
        if (level == 10)
        {
            levelText = "Max";
            cost = 0;
        }
        else
        {
            levelText = level.ToString();
        }

        if (sLevel == 3)
        {
            sLevelText = "Max";
            sCost = 0;
        }
        else
        {
            sLevelText= sLevel.ToString();
        }

        skillNameText.text = ($"{skillName}")
            .Replace("{lv}", levelText)
            .Replace("{slv}", sLevelText);
        skillDescriptionText.text = skillDescription
            .Replace("{dmg}", dmg.ToString())
            .Replace("{spd}", spd.ToString("F2"))
            .Replace("{interval}", interval.ToString("F2"))
            .Replace("{crit}", crit.ToString())
            .Replace("{skillDmg}", sDmg.ToString())
            .Replace("{dmgMul}", dmgMul.ToString());

        if (!isSkill)
        {
            upgradeCostText.text = cost.ToString();
            costIcon.sprite = goldIcon;
        }
        else
        {
            upgradeCostText.text = sCost.ToString();
            costIcon.sprite = soulIcon;
        }
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
