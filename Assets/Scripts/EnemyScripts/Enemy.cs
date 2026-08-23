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


    void Awake()
    {
        if (enemyUI == null)
        {
            // Get enemyUI component and subscribe to OnReachedPlayer event
            enemyUI = GetComponent<EnemyUI>();
            enemyUI.OnReachedPlayer += HandleReachedPlayer;
        }
    }

    // Unsubscribe from events before the object is destroyed
    private void OnDestroy()
    {
        if (enemyUI != null) enemyUI.OnReachedPlayer -= HandleReachedPlayer;
        if (countdownTimer != null) countdownTimer.OnTimerStop -= HandleTimerStop;
    }

    // Initialize the enemy prefab and calculates its stats and rewards
    public void Initialize(PlayerCombat combat, CountdownTimer timer, int level, string type)
    {
        playerCombat = combat;
        countdownTimer = timer;
        countdownTimer.OnTimerStop += HandleTimerStop;

        CalculateHealth(level, type);
        CalculateGoldDrop();
        CalcualateSoulsDrop(level, type);
    }

    // Calculate enemy/boss health based on level
    private void CalculateHealth(int level, string type)
    {
        if (type == "Enemy")
        {
            // Calculate enemy health with 5% increase per level
            maxHealth = Mathf.RoundToInt(500 * Mathf.Pow(1.05f, level - 1));
            currentHealth = maxHealth;
        }
        else if(type == "Boss")
        {
            // Calculate boss health with 8% increase per level
            maxHealth = Mathf.RoundToInt(1000 * Mathf.Pow(1.08f, level - 1));
            currentHealth = maxHealth;
        }
    }

    // Calculate enemy/boss gold drop based on level
    private int CalculateGoldDrop()
    {
        goldDrop = Random.Range((maxHealth / 20) - 3, (maxHealth / 20) + 3);
        return goldDrop;
    }

    // Calculate enemy/boss souls drop based on level
    private int CalcualateSoulsDrop(int level, string type)
    {
        // temporary formula will be changed in future
        if (type == "Enemy")
        {
            soulsDrop = 0;
        }
        else if (type == "Boss")
        {
            // Increase souls drop by 1 every 5 levels
            soulsDrop = 1 + ((level - 1) / 5);
        }
        return soulsDrop;
    }

    // Reduce health and if health reaches 0, it dies
    public void TakeDamage(int damage, PlayerStats stats)
    {
        playerStats = stats;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // Give rewards, end attack and destroy the object
    public void Die()
    {
        DropReward();
        GameManager.Instance.EndAttack();
        Destroy(gameObject);
        GameManager.Instance.EnemyDefeated();
    }

    // End attack and destroy the object without giving rewards
    public void RunAway()
    {
        GameManager.Instance.EndAttack();
        Destroy(gameObject);
    }

    // Add rewards to the temporary resource holder
    public void DropReward()
    {
        ResourceManager.Instance.AddTempGold(goldDrop);
        ResourceManager.Instance.AddTempSouls(soulsDrop);
    }

    // Start attack when enemy reaches player
    private void HandleReachedPlayer()
    {
        GameManager.Instance.StartAttack();
        playerCombat.SetTarget(this);
    }

    // Enemy run away when timer stop
    private void HandleTimerStop()
    {
        RunAway();
    }
}
