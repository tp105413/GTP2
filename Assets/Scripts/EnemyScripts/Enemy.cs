using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerStats playerStats;

    private int maxHealth;
    private int currentHealth;
    private int goldDrop = 0;
    private int soulsDrop = 0;

    public void Initialize(GameManager gm, int level, string type)
    {
        gameManager = gm;

        CalculateHealth(level, type);
        CalculateGoldDrop(level, type);
        CalcualateSoulsDrop(level, type);
        Debug.Log(type + " Health: " + currentHealth);
    }

    private void CalculateHealth(int level, string type)
    {
        // will add in formula, current just for testing
        if (type == "Enemy")
        {
            maxHealth = 50 + (level - 1) * 10;
            currentHealth = maxHealth;
        }
        else if(type == "Boss")
        {
            maxHealth = 100 + (level - 1) * 10;
            currentHealth = maxHealth;
        }
    }

    private int CalculateGoldDrop(int level, string type)
    {
        // temporary formula will be changed in future
        if (type == "Enemy")
        {
            goldDrop = 10 * level;
        }
        else if (type == "Boss")
        {
            goldDrop = 30 * level;
        }
        return goldDrop;
    }

    private int CalcualateSoulsDrop(int level, string type)
    {
        // temporary formula will be changed in future
        if (type == "Enemy")
        {
            soulsDrop = 0;
        }
        else if (type == "Boss")
        {
            soulsDrop = 1 * level;
        }
        return soulsDrop;
    }

    // need clarify code logic
    public void TakeDamage(int damage, PlayerStats stats)
    {
        playerStats = stats;

        Debug.Log("Enemy is taking " + damage + " damage!");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy dead!");
        DropReward();
        Destroy(gameObject);
        gameManager.EnemyDefeated();
    }

    public void DropReward()
    {
        playerStats.AddGold(goldDrop);
        playerStats.AddSouls(soulsDrop);
    }
}
