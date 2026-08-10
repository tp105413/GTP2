using System;
using System.Threading;
using UnityEngine;

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
        countdownTimer.StartTimer();
        enemySpawner.SpawnEnemy(gameLevel);
    }

    public void OnStopButtonClick()
    {
        countdownTimer.StopTimer();
        RemainingEnemyCountReset();
    }

    public void OnPauseButtonClick()
    {
        countdownTimer.PauseTimer();
    }

    public void GameLevelUp()
    {
        gameLevel++;
        Debug.Log("Game Level: " + gameLevel);
    }

    public void EnemyDefeated()
    {
        remainingEnemyCount--;

        if(remainingEnemyCount > 0)
        {
            enemySpawner.SpawnEnemy(gameLevel);
        }
        else if (!bossSpawned)
        {
            enemySpawner.SpawnBoss(gameLevel);
            bossSpawned = true;
        }
        else if(bossSpawned)
        {
            GameLevelUp();
            // every 5 level + 1 enemy
            RemainingEnemyCountReset();
            bossSpawned = false;
            enemySpawner.SpawnEnemy(gameLevel);
        }
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

    public void RemainingEnemyCountReset()
    {
        Debug.Log("Remaining reset: " + remainingEnemyCount);
        remainingEnemyCount = 5 + (gameLevel - 1) / 5;
    }
}
