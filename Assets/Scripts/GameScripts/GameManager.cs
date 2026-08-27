using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameRunning = false;
    public bool isAttacking = false;
    public float worldMoveSpeed = 0f;

    public int gameLevel = 1;
    public int remainingEnemyCount = 5;
    public bool bossSpawned = false;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private UIManager uiManager;


    private void Awake()
    {
        // Check only 1 GameManager instance exists and destroy duplicates
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
        // Exit when Timer is set to 0
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
        uiManager.UpdateLevelUI();
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

    // Start and initialize the game
    public void StartGame()
    {
        isGameRunning = true;
        countdownTimer.StartTimer();
        enemySpawner.SpawnEnemy(gameLevel);
        playerUI.StartRunningAnimation();
    }

    // Stop the game and reset its state
    public void StopGame()
    {
        isGameRunning = false;
        countdownTimer.StopTimer();
        RemainingEnemyCountReset();
        playerUI.StopRunningAnimation();

        ResourceManager.Instance.CalculateReward();
    }

    // Pause the game and preserve its current state
    public void PauseGame()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            countdownTimer.PauseTimer(); // Pause the timer
            playerUI.StopRunningAnimation();
        }
        else
        {
            isGameRunning = true;
            countdownTimer.PauseTimer(); // Resume the timer
            enemySpawner.SpawnEnemy(gameLevel);
            playerUI.StartRunningAnimation();
        }
    }

    // Reset and increase enemy count by 1 every 3 levels
    public void RemainingEnemyCountReset()
    {
        remainingEnemyCount = 5 + (gameLevel - 1) / 3;
    }

    // Set the player attack state to active
    public void StartAttack()
    {
        isAttacking = true;
        playerUI.StartAttackAnimation();
    }

    // Set the player attack state to inactive
    public void EndAttack()
    {
        isAttacking = false;
        playerUI.StopAttackAnimation();
    }
}
