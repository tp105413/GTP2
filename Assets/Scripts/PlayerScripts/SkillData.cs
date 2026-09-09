using UnityEngine;

[System.Serializable]
public class SkillData
{
    public SkillType skillType;

    [Header("Unlock Requirement")]
    public SkillType requiredStats;
    public int requiredLevel;

    [Header("Skill")]
    public bool isUnlocked;
    public int level;
    public int maxLevel;

    [Header("Costs")]
    public int unlockCost;
    public int[] upgradeCosts;

    [Header("Damage")]
    public float[] damageMultiplier;
}
