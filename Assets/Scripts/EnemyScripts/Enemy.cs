using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private float stopAtPositionX = -1f;
    private bool hasReachedPlayer = false;

    private int maxHealth;
    private int currentHealth;
    private int goldDrop = 0;
    private int soulsDrop = 0;

    public void Initialize(PlayerCombat combat, int level, string type)
    {
        playerCombat = combat;

        CalculateHealth(level, type);
        CalculateGoldDrop(level, type);
        CalcualateSoulsDrop(level, type);
        Debug.Log(type + " Health: " + currentHealth);
    }

    void Update()
    {
        if (hasReachedPlayer) return;

        if(transform.position.x > stopAtPositionX)
        {
            transform.position += Vector3.left * GameManager.Instance.worldMoveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(stopAtPositionX, transform.position.y, transform.position.z);
            hasReachedPlayer = true;

            GameManager.Instance.StartAttack();
            playerCombat.SetTarget(this);
        }
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
        GameManager.Instance.EndAttack();
        Destroy(gameObject);
        GameManager.Instance.EnemyDefeated();
    }

    public void DropReward()
    {
        playerStats.AddGold(goldDrop);
        playerStats.AddSouls(soulsDrop);
    }
}
