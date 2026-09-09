using UnityEngine;

public class SkillUpgrade : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private SkillData[] skills;


    // Get a skill by its type
    public SkillData GetSkill(SkillType skillType)
    {
        foreach (SkillData skill in skills)
        {
            if (skill.skillType == skillType) return skill;
        }

        return null;
    }

    // Unlock a skill if requirements are met
    public void UnlockSkill(SkillData skill)
    {
        if (skill.isUnlocked) return;

        // Check required unlock level
        int currentLevel = playerStats.GetSkillLevel(skill.requiredStats);
        if (currentLevel < skill.requiredLevel) return;

        if (playerStats.PlayerSouls < skill.unlockCost) return;

        playerStats.SpendPlayerSouls(skill.unlockCost);

        Debug.Log("Power Strike Unlocked!");
        skill.isUnlocked = true;
        skill.level = 1;
    }

    // Upgrade a skill
    public void UpgradeSkill(SkillType skillType)
    {
        SkillData skill = GetSkill(skillType);

        if (skill == null) return;

        // Try to unlock if not unlock yet
        if (!skill.isUnlocked)
        {
            UnlockSkill(skill);
            return;
        }

        // Check if already max level
        if (skill.level >= skill.maxLevel) return;

        int upgradeCost = skill.upgradeCosts[skill.level - 1];
        if (playerStats.PlayerSouls < upgradeCost) return;

        playerStats.SpendPlayerSouls(upgradeCost);
        skill.level++;

        Debug.Log("Power Strike upgraded to Level " + skill.level);
    }

    // Get skill level
    public int GetSkillLevel(SkillType skilltype)
    {
        SkillData skill = GetSkill(skilltype);

        if (skill == null) return 0;

        return skill.level;
    }

    // Get the skill damage
    public (int damage, int multiplier) GetSkillDamage(SkillType skilltype)
    {
        SkillData skill = GetSkill(skilltype);

        if (skill == null) return (0, 0);

        float multiplier = skill.damageMultiplier[skill.level - 1];
        int damage = Mathf.RoundToInt(playerStats.AttackDamage * multiplier);

        // Format multiplier to percentage for display
        multiplier *= 100;

        return (damage, (int)multiplier);
    }
}
