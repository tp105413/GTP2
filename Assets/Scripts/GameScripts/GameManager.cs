using System;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isAttacking = false;
    public float worldMoveSpeed = 2f;

    private int gameLevel = 1;
    private int remainingEnemyCount = 3;
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
            // increase enemy count base on level here
            GameLevelUp();
            remainingEnemyCount = 3;
            bossSpawned = false;
            enemySpawner.SpawnEnemy(gameLevel);
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
