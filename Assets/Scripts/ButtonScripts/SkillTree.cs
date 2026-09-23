using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private StatsDisplay statsDisplay;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private StatsUpgrade statsUpgrade;
    [SerializeField] private SkillUpgrade skillUpgrade;

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
        bool isSkill = button.IsSkill;

        // Base stats value 
        int level = playerStats.GetSkillLevel(button.SkillType);
        int dmg = playerStats.GetSkillIntValue(button.SkillType);
        float spd = playerStats.GetSkillFloatValue(button.SkillType);
        float interval = playerStats.GetSkillFloatValue(button.SkillType);
        int crit = playerStats.GetSkillIntValue(button.SkillType);
        int cost = statsUpgrade.BaseLevelStatsGoldCost * level;

        // Skills stats value
        int sLevel = skillUpgrade.GetSkillLevel(button.SkillType);
        int scost = skillUpgrade.GetSkillCost(button.SkillType);
        (int sDmg, int dmgMul) = skillUpgrade.GetSkillDamage(button.SkillType);

        statsDisplay.ShowSkill(button.SkillName, button.SkillDescription, isSkill, level, cost, dmg, spd, interval, crit, sLevel, scost, sDmg, dmgMul);
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
            case SkillType.PowerStrike:
                skillUpgrade.UpgradeSkill(SkillType.PowerStrike);
                break;
        }

        // After update refresh display UI
        RefreshSkillValue(ButtonSelected);

        // Update skill level image
        if (ButtonSelected.IsSkill)
        {
            int newLevel = skillUpgrade.GetSkillLevel(skillType);
            ButtonSelected.UpdateButtonLevelImage(newLevel);
        }

        // If skill is unlocked, update the button UI
        SkillData skill = skillUpgrade.GetSkill(skillType);
        if(skill != null && skill.isUnlocked)
        {
            ButtonSelected.UnlockButton();
        }
    }
}
