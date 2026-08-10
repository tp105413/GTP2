using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private EnemyUI enemyUI;

    private CountdownTimer countdownTimer;

    private int maxHealth;
    private int currentHealth;
    private int goldDrop = 0;
    private int soulsDrop = 0;

    public void Initialize(PlayerCombat combat, CountdownTimer timer, int level, string type)
    {
        playerCombat = combat;
        countdownTimer = timer;
        countdownTimer.OnTimerStop += HandleTimerStop;

        CalculateHealth(level, type);
        CalculateGoldDrop(level, type);
        CalcualateSoulsDrop(level, type);
        Debug.Log(type + " Health: " + currentHealth);
    }

    void Awake()
    {
        if(enemyUI == null)
        {
            enemyUI = GetComponent<EnemyUI>();
            // subscribe function to action, when action is invoked will run the function
            enemyUI.OnReachedPlayer += HandleReachedPlayer;
        }
    }

    // unsubscribe automatically
    private void OnDestroy()
    {
        if (enemyUI != null) enemyUI.OnReachedPlayer -= HandleReachedPlayer;
        if(countdownTimer != null) countdownTimer.OnTimerStop -= HandleTimerStop;
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

    public void RunAway()
    {
        Debug.Log("Enemy run away!");
        GameManager.Instance.EndAttack();
        Destroy(gameObject);
    }

    public void DropReward()
    {
        playerStats.AddGold(goldDrop);
        playerStats.AddSouls(soulsDrop);
    }

    private void HandleReachedPlayer()
    {
        GameManager.Instance.StartAttack();
        playerCombat.SetTarget(this);
    }

    private void HandleTimerStop()
    {
        RunAway();
    }
}
