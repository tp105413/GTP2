using UnityEngine;

public class StatsUpgrade : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private int basePlayerLevelSoulsCost = 1;
    private int baseLevelStatsGoldCost = 5;

    public void PlayerLevelUp()
    {
        // maybe need to balance
        int upgradeCost = basePlayerLevelSoulsCost * playerStats.PlayerLevel;

        if(playerStats.Souls >= upgradeCost && playerStats.CanLevelUp())
        {
            playerStats.SpendSouls(upgradeCost);
            playerStats.PlayerLevelUp();
        }
    }

    public void UpgradeAttackDamage()
    {
        Debug.Log("Button clicked!");

        int level = playerStats.AttackDamageLevel;
        // implement upgrade cost increment base on level
        int upgradeCost = level * baseLevelStatsGoldCost;

        if (playerStats.Gold >= upgradeCost && level < playerStats.PlayerLevel)
        {
            playerStats.SpendGold(upgradeCost);

            // need balance
            playerStats.UpgradeAttackDamage(5*level);
        }
    }

    public void UpgradeAttackInterval()
    {
        int level = playerStats.AttackIntervalLevel;
        // implement upgrade cost increment base on level
        int upgradeCost = level * baseLevelStatsGoldCost;

        if (playerStats.Gold >= upgradeCost && level < playerStats.PlayerLevel)
        {
            playerStats.SpendGold(upgradeCost);

            // upgrade formula need balance (maybe)
            playerStats.UpgradeAttackInterval(0.1f);
        }
    }

    public void UpgradeAttackSpeed()
    {
        int level = playerStats.AttackSpeedLevel;
        // implement upgrade cost increment base on level
        int upgradeCost = level * baseLevelStatsGoldCost;

        if (playerStats.Gold >= upgradeCost && level < playerStats.PlayerLevel)
        {
            playerStats.SpendGold(upgradeCost);

            // upgrade formula need balance
            playerStats.UpgradeAttackSpeed(0.1f);
        }
    }

    public void UpgradeCriticalChance()
    {
        int level = playerStats.CriticalChanceLevel;
        // implement upgrade cost increment base on level
        int upgradeCost = level * baseLevelStatsGoldCost;

        if (playerStats.Gold >= upgradeCost && level < playerStats.PlayerLevel)
        {
            playerStats.SpendGold(upgradeCost);

            // upgrade formula need balance
            playerStats.UpgradeCriticalChance(5);
        }
    }

    public void UpgradeCriticalDamage()
    {
        int level = playerStats.CriticalDamageLevel;
        // implement upgrade cost increment base on level
        int upgradeCost = level * baseLevelStatsGoldCost;

        if (playerStats.Gold >= upgradeCost && level < playerStats.PlayerLevel)
        {
            playerStats.SpendGold(upgradeCost);

            // upgrade formula need balance
            playerStats.UpgradeCriticalDamage(10);
        }
    }
}
