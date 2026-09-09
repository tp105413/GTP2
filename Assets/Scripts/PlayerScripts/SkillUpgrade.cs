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
    }

    // Get skill level
    public int GetSkillLevel(SkillType skilltype)
    {
        SkillData skill = GetSkill(skilltype);

        if (skill == null) return 0;

        return skill.level;
    }

    public int GetSkillCost(SkillType skilltype)
    {
        SkillData skill = GetSkill(skilltype);

        if (skill == null) return 0;

        // If skill is not unlocked show unlock cost
        if (!skill.isUnlocked) return skill.unlockCost;

        if (skill.level >= skill.maxLevel) return 0;

        return skill.upgradeCosts[skill.level - 1];
    }

    // Get the skill damage
    public (int damage, int multiplier) GetSkillDamage(SkillType skilltype)
    {
        SkillData skill = GetSkill(skilltype);

        if (skill == null) return (0, 0);

        // Prevent skill level become -1
        int index = Mathf.Max(skill.level - 1, 0);

        float multiplier = skill.damageMultiplier[index];
        int damage = Mathf.RoundToInt(playerStats.AttackDamage * multiplier);

        // Format multiplier to percentage for display
        multiplier *= 100;

        return (damage, (int)multiplier);
    }
}
