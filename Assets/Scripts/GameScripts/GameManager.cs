using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // control whole game function!
    public bool isGameRunning = false;

    public bool isAttacking = false;
    public float worldMoveSpeed = 0f;

    private int gameLevel = 1;
    private int remainingEnemyCount = 5;
    private bool bossSpawned = false;
    private Animator playerAnimator;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private ScrollingAnimation scrollingAnimation;

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
        // will change
        if (countdownTimer.TimeRemaining == TimeSpan.Zero) return;

        // coroutine
        StartCoroutine(WaitForScrollCompleted());

        //countdownTimer.StartTimer();
        //enemySpawner.SpawnEnemy(gameLevel);
    }

    IEnumerator WaitForScrollCompleted()
    {
        // need to be modify
        yield return new WaitForSeconds(1.5f);
        countdownTimer.StartTimer();
        enemySpawner.SpawnEnemy(gameLevel);

        //playerAnimator.Play("walking", 1, 0f); // Animator
        //playerAnimator.SetTrigger("ToAttack");  //Animator
    } 

    public void OnStopButtonClick()
    {
        countdownTimer.StopTimer();
        RemainingEnemyCountReset();
    }

    public void OnPauseButtonClick()
    {
        if (!countdownTimer.IsTimerPaused)
        {
            countdownTimer.PauseTimer();
        }
        else
        {
            countdownTimer.PauseTimer();
            enemySpawner.SpawnEnemy(gameLevel);
        }
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
