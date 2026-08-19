using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int gold = 0;
    [SerializeField] private int souls = 0;

    // Read-only from other classes
    public int Gold => gold;
    public int Souls => souls;
    
    private int attackDamage = 25;
    private float attackInterval = 2f;
    private float attackSpeed = 1f;
    private int criticalChance = 0;
    private int criticalDamage = 0;

    // Read-only from other classes
    public int AttackDamage => attackDamage;
    public float AttackInterval => attackInterval;
    public float AttackSpeed => attackSpeed;
    public int CriticalChance => criticalChance;
    public int CriticalDamage => criticalDamage;

    private int playerLevel = 1;
    private int attackDamageLevel = 1;
    private int attackIntervalLevel = 1;
    private int attackSpeedLevel = 1;
    private int criticalChanceLevel = 1;
    private int criticalDamageLevel = 1;

    // Read-only from other classes
    public int PlayerLevel => playerLevel;
    public int AttackDamageLevel => attackDamageLevel;
    public int AttackIntervalLevel => attackIntervalLevel;
    public int AttackSpeedLevel => attackSpeedLevel;
    public int CriticalChanceLevel => criticalChanceLevel;
    public int CriticalDamageLevel => criticalDamageLevel;

    public void AddGold(int amount)
    {
        gold += amount;
        ResourceManager.Instance.UpdateGold(gold);
    }

    public void AddSouls(int amount)
    {
        souls += amount;
        ResourceManager.Instance.UpdateSoul(souls);

    }

    public void SpendGold(int amount)
    {
        gold -= amount;
    }

    public void SpendSouls(int amount)
    {
        souls -= amount;
    }

    public void PlayerLevelUp()
    {
        playerLevel++;
        Debug.Log("Player Level Up");
    }

    public void UpgradeAttackDamage(int amount)
    {
        attackDamage += amount;
        attackDamageLevel++;
        Debug.Log("AttackDamageLevelUp");
    }

    public void UpgradeAttackInterval(float amount)
    {
        attackInterval -= amount;
        attackIntervalLevel++;
        Debug.Log("AttackIntervalLevelUp");
    }

    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeed += amount;
        attackSpeedLevel++;
        Debug.Log("AttackSpeedLevelUp");
    }

    public void UpgradeCriticalChance(int amount)
    {
        criticalChance += amount;
        criticalChanceLevel++;
        Debug.Log("CriticalChanceLevelUp");
    }

    public void UpgradeCriticalDamage(int amount)
    {
        criticalDamage += amount;
        criticalDamageLevel++;
        Debug.Log("CriticalDamageLevelUp");
    }

    public bool CanLevelUp()
    {
        return attackDamageLevel == playerLevel &&
            attackIntervalLevel == playerLevel &&
            attackSpeedLevel == playerLevel &&
            criticalChanceLevel == playerLevel &&
            criticalDamageLevel == playerLevel;
    }
}
