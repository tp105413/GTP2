using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private StatsDisplay statsDisplay;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private StatsUpgrade statsUpgrade;

    private SkillTreeButton buttonSelected;

    public SkillTreeButton ButtonSelected => buttonSelected;


    // Select a skill button and refresh and display current state
    public void SelectButton(SkillTreeButton button)
    {
        if(buttonSelected != null)
        {
            buttonSelected.SetSelected(false);
        }

        buttonSelected = button;
        buttonSelected.SetSelected(true);

        RefreshSkillValue(button);
    }

    // Refresh the selected skills values
    public void RefreshSkillValue(SkillTreeButton button)
    {
        int level = playerStats.GetSkillLevel(button.SkillType);
        int dmg = playerStats.GetSkillIntValue(button.SkillType);
        float spd = playerStats.GetSkillFloatValue(button.SkillType);
        float interval = playerStats.GetSkillFloatValue(button.SkillType);
        int crit = playerStats.GetSkillIntValue(button.SkillType);
        int cost = statsUpgrade.BaseLevelStatsGoldCost * level;

        statsDisplay.ShowSkill(button.SkillName, button.SkillDescription, level, cost, dmg, spd, interval, crit);
    }

    // Upgrades the selected skill
    public void UpgradeSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.AttackDamage:
                statsUpgrade.UpgradeAttackDamage();
                break;
            case SkillType.AttackSpeed:
                statsUpgrade.UpgradeAttackSpeed();
                break;
            case SkillType.AttackInterval:
                statsUpgrade.UpgradeAttackInterval();
                break;
            case SkillType.CriticalChance:
                statsUpgrade.UpgradeCriticalChance();
                break;
            case SkillType.CriticalDamage:
                statsUpgrade.UpgradeCriticalDamage();
                break;
        }

        // After update refresh display UI
        RefreshSkillValue(ButtonSelected);
    }
}
