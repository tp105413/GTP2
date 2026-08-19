using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isAttacking = false;
    public float worldMoveSpeed = 0f;

    private int gameLevel = 1;
    private int remainingEnemyCount = 5;
    private bool bossSpawned = false;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CountdownTimer countdownTimer;


    private void Awake()
    {
        // Check only 1 Game Manager instance exists and destroy duplicates
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnStartButtonClick()
    {
        // Exit when timer is set to 0
        if (countdownTimer.TimeRemaining == TimeSpan.Zero) return;

        StartCoroutine(WaitForScrollCompleted());
    }

    // Start the game after the scroll animation is completed
    IEnumerator WaitForScrollCompleted()
    {
        yield return new WaitForSeconds(1.5f);
        StartGame();
    } 

    public void OnStopButtonClick()
    {
        StopGame();
    }

    public void OnPauseButtonClick()
    {
        PauseGame();
    }

    public void GameLevelUp()
    {
        gameLevel++;
    }

    // Handle enemy defeat and spawn the next enemy, boss or level
    public void EnemyDefeated()
    {
        remainingEnemyCount--;

        // Spawn the next enemy if enemies remain
        if(remainingEnemyCount > 0)
        {
            enemySpawner.SpawnEnemy(gameLevel);
        }
        // Spawn boss when all enemies are defeated
        else if (!bossSpawned)
        {
            enemySpawner.SpawnBoss(gameLevel);
            bossSpawned = true;
        }
        // Level up and start the next wave after boss is defeated
        else if(bossSpawned)
        {
            GameLevelUp();
            RemainingEnemyCountReset();
            bossSpawned = false;
            enemySpawner.SpawnEnemy(gameLevel);
        }
    }

    public void StartGame()
    {
        countdownTimer.StartTimer();
        enemySpawner.SpawnEnemy(gameLevel);
    }

    public void StopGame()
    {
        countdownTimer.StopTimer();
        RemainingEnemyCountReset();
    }

    public void PauseGame()
    {
        if (!countdownTimer.IsTimerPaused)
        {
            countdownTimer.PauseTimer(); // Pause the timer
        }
        else
        {
            countdownTimer.PauseTimer();
            enemySpawner.SpawnEnemy(gameLevel); // Resume the timer
        }
    }

    // Reset and increase enemy count by 1 every 5 levels
    public void RemainingEnemyCountReset()
    {
        remainingEnemyCount = 5 + (gameLevel - 1) / 5;
    }

    public void StartAttack()
    {
        isAttacking = true;
        // attack animation call playerUI here
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
