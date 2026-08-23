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


    // Add gold and souls from calculated rewards
    public void TransferCalculatedReward (int goldAmount, int soulsAmount)
    {
        playerGold += goldAmount;
        OnGoldChanged?.Invoke(playerGold);
        playerSouls += soulsAmount;
        OnSoulsChanged?.Invoke(playerSouls);
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
    }

    // Increase attack speed
    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeed += amount;
        attackSpeedLevel++;
        playerUI.SetAttackSpeed(attackSpeed);
    }

    // Reduce attack interval
    public void UpgradeAttackInterval(float amount)
    {
        attackInterval -= amount;
        attackIntervalLevel++;
    }

    // Increase critical chance
    public void UpgradeCriticalChance(int amount)
    {
        criticalChance += amount;
        criticalChanceLevel++;
    }

    // Increase critical damage
    public void UpgradeCriticalDamage(int amount)
    {
        criticalDamage += amount;
        criticalDamageLevel++;
    }
}
