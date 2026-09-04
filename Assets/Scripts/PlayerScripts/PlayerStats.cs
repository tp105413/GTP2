using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;

    [SerializeField] private int playerGold = 0;
    [SerializeField] private int playerSouls = 0;

    // Read-only from other classes
    public int PlayerGold => playerGold;
    public int PlayerSouls => playerSouls;

    [SerializeField] private int attackDamage = 100;
    [SerializeField] private float attackSpeed = 1.0f;
    [SerializeField] private float attackInterval = 2.5f;
    [SerializeField] private int criticalChance = 0;
    [SerializeField] private int criticalDamage = 0;

    // Read-only from other classes
    public int AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public float AttackInterval => attackInterval;
    public int CriticalChance => criticalChance;
    public int CriticalDamage => criticalDamage;

    private int attackDamageLevel = 1;
    private int attackSpeedLevel = 1;
    private int attackIntervalLevel = 1;
    private int criticalChanceLevel = 1;
    private int criticalDamageLevel = 1;

    // Read-only from other classes
    public int AttackDamageLevel => attackDamageLevel;
    public int AttackSpeedLevel => attackSpeedLevel;
    public int AttackIntervalLevel => attackIntervalLevel;
    public int CriticalChanceLevel => criticalChanceLevel;
    public int CriticalDamageLevel => criticalDamageLevel;

    // Invoked when player gold/souls change
    public event Action<int> OnGoldChanged;
    public event Action<int> OnSoulsChanged;

    // Invoke when player stats value change
    public event Action OnStatsValueChanged;


    // Add gold and souls from calculated rewards
    public void TransferCalculatedReward(int goldAmount, int soulsAmount)
    {
        playerGold += goldAmount;
        OnGoldChanged?.Invoke(playerGold);
        playerSouls += soulsAmount;
        OnSoulsChanged?.Invoke(playerSouls);


        //Record gold and soul that player earned from the battle  (but this one save the value to disk)
        int totalGold = PlayerPrefs.GetInt("TotalGoldEarned", 0);
        totalGold += goldAmount;
        PlayerPrefs.SetInt("TotalGoldEarned", totalGold);

        int totalSouls = PlayerPrefs.GetInt("TotalSoulEarned", 0);
        totalSouls += soulsAmount;
        PlayerPrefs.SetInt("TotalSoulEarned", totalSouls);
    }

    public void SpendPlayerGold(int amount)
    {
        playerGold -= amount;
        OnGoldChanged?.Invoke(playerGold);
    }

    public void SpendPlayerSouls(int amount)
    {
        playerSouls -= amount;
        OnSoulsChanged?.Invoke(playerSouls);
    }

    // Increase attack damage
    public void UpgradeAttackDamage(int amount)
    {
        attackDamage = amount;
        attackDamageLevel++;
        OnStatsValueChanged?.Invoke();
    }

    // Increase attack speed
    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeed += amount;
        attackSpeedLevel++;
        playerUI.SetAttackSpeed(attackSpeed);
        OnStatsValueChanged?.Invoke();
    }

    // Reduce attack interval
    public void UpgradeAttackInterval(float amount)
    {
        attackInterval -= amount;
        attackIntervalLevel++;
        OnStatsValueChanged?.Invoke();
    }

    // Increase critical chance
    public void UpgradeCriticalChance(int amount)
    {
        criticalChance += amount;
        criticalChanceLevel++;
        OnStatsValueChanged?.Invoke();
    }

    // Increase critical damage
    public void UpgradeCriticalDamage(int amount)
    {
        criticalDamage += amount;
        criticalDamageLevel++;
        OnStatsValueChanged?.Invoke();
    }

    // Return current level of specified skill
    public int GetSkillLevel(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.AttackDamage: return AttackDamageLevel;
            case SkillType.AttackSpeed: return AttackSpeedLevel;
            case SkillType.AttackInterval: return AttackIntervalLevel;
            case SkillType.CriticalChance: return CriticalChanceLevel;
            case SkillType.CriticalDamage: return CriticalDamageLevel;

            default: return 0;
        }
    }

    // Return int value for the specified skill
    public int GetSkillIntValue(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.AttackDamage: return AttackDamage;
            case SkillType.CriticalChance: return CriticalChance;
            case SkillType.CriticalDamage: return CriticalDamage;

            default: return 0;
        }
    }

    // Return float value for the specified skill
    public float GetSkillFloatValue(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.AttackSpeed: return AttackSpeed;
            case SkillType.AttackInterval: return AttackInterval;

            default: return 0;
        }
    }
}
