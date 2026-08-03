using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int gold = 0;
    private int souls = 0;

    // read only
    public int Gold => gold;
    public int Soul => souls;
    
    private int attackDamage = 10;
    private float attackInterval = 1f;
    private float attackSpeed = 1f;
    private float criticalChance = 0f;
    private float criticalDamage = 0f;

    public int AttackDamage => attackDamage;
    public float AttackInterval => attackInterval;
    public float AttackSpeed => attackSpeed;
    public float CriticalChance => criticalChance;
    public float CriticalDamage => criticalDamage;

    private int playerLevel = 1;
    private int attackDamageLevel = 1;
    private int attackIntervalLevel = 1;
    private int attackSpeedLevel = 1;
    private int criticalChanceLevel = 1;
    private int criticalDamageLevel = 1;

    public int PlayerLevel => playerLevel;
    public int AttackDamageLevel => attackDamageLevel;
    public int AttackIntervalLevel => attackIntervalLevel;
    public int AttackSpeedLevel => attackSpeedLevel;
    public int CriticalChanceLevel => criticalChanceLevel;
    public int CriticalDamageLevel => criticalDamageLevel;

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void AddSouls(int amount)
    {
        souls += amount;
    }
}
