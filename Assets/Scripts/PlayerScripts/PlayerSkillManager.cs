using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private SkillUpgrade skillUpgrade;

    private int attackCount = 0;

    // Check if Power Strike is available to use
    public bool CheckPowerStrike()
    {
        SkillData skill = skillUpgrade.GetSkill(SkillType.PowerStrike);

        // Check if Power Strike is unlocked
        if (skill == null || !skill.isUnlocked) return false;

        attackCount++;

        // After every 3 attack can use Power Strike 
        if(attackCount >= 3)
        {
            attackCount = 0;
            return true;
        }

        return false;
    }

    // Get the damage of Power Strike Skill 
    public int GetPowerStrikeDamage()
    {
        (int damage, int mul) = skillUpgrade.GetSkillDamage(SkillType.PowerStrike);
        return damage;
    }
}
