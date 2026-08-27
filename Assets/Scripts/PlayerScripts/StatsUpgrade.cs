using UnityEngine;

public class StatsUpgrade : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private int baseLevelStatsGoldCost = 100;

    public int BaseLevelStatsGoldCost => baseLevelStatsGoldCost;


    public void UpgradeAttackDamage()
    {
        int level = playerStats.AttackDamageLevel;
        int value = playerStats.AttackDamage;
        int upgradeCost = level * baseLevelStatsGoldCost;

        // Check if player has enough gold
        if (playerStats.PlayerGold >= upgradeCost && playerStats.AttackDamageLevel < 10)
        {
            playerStats.SpendPlayerGold(upgradeCost);

            // Increase attack damage by 15% per level
            int upgradeAmount = Mathf.RoundToInt(value * 1.15f);
            playerStats.UpgradeAttackDamage(upgradeAmount);
        }
    }

    public void UpgradeAttackSpeed()
    {
        int level = playerStats.AttackSpeedLevel;
        int upgradeCost = level * baseLevelStatsGoldCost;

        // Check if player has enough gold
        if (playerStats.PlayerGold >= upgradeCost && playerStats.AttackSpeedLevel < 10)
        {
            playerStats.SpendPlayerGold(upgradeCost);

            // Increase attack speed by 0.05 seconds per level
            playerStats.UpgradeAttackSpeed(0.05f);
        }
    }

    public void UpgradeAttackInterval()
    {
        int level = playerStats.AttackIntervalLevel;
        int upgradeCost = level * baseLevelStatsGoldCost;

        // Check if player has enough gold
        if (playerStats.PlayerGold >= upgradeCost && playerStats.AttackIntervalLevel < 10)
        {
            playerStats.SpendPlayerGold(upgradeCost);

            // Reduce attack cooldown by 0.1 seconds per level
            playerStats.UpgradeAttackInterval(0.1f);
        }
    }

    public void UpgradeCriticalChance()
    {
        int level = playerStats.CriticalChanceLevel;
        int upgradeCost = level * baseLevelStatsGoldCost;

        // Check if player has enough gold
        if (playerStats.PlayerGold >= upgradeCost && playerStats.CriticalChanceLevel < 10)
        {
            playerStats.SpendPlayerGold(upgradeCost);

            // Increase critical chance by 5% per level
            playerStats.UpgradeCriticalChance(5);
        }
    }

    public void UpgradeCriticalDamage()
    {
        int level = playerStats.CriticalDamageLevel;
        int upgradeCost = level * baseLevelStatsGoldCost;

        // Check if player has enough gold
        if (playerStats.PlayerGold >= upgradeCost && playerStats.CriticalDamageLevel < 10)
        {
            playerStats.SpendPlayerGold(upgradeCost);

            // Increase critical damage by 10% per level
            playerStats.UpgradeCriticalDamage(10);
        }
    }
}
