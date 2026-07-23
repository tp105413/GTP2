using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    private GameManager gameManager;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void InitializeHealth(int level)
    {
        CalculateHealth(level);
        Debug.Log("Health: " + currentHealth);
    }

    private void CalculateHealth(int level)
    {
        // will add in formula, current just for testing
        maxHealth = 50 + (level - 1) * 10;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
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
        Destroy(gameObject);
        gameManager.GameLevelUp();
    }

    public void SetGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    public void DropReward()
    {
        
    }
}
