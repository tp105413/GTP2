using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameRunning = false;
    public bool isAttacking = false;
    public float worldMoveSpeed = 0f;

    public int gameLevel = 1;
    public int remainingEnemyCount = 5;
    public bool bossSpawned = false;
    public bool isLevelFailed = false;

    private Coroutine bossTimerCoroutine;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private UIManager uiManager;

    public event Action OnLevelFailed;


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

        uiManager.ToggleObjectUI();

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
        PauseGame(true);
    }

    public void GameLevelUp()
    {
        gameLevel++;
        uiManager.UpdateLevelUI();
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 0);
        PlayerPrefs.SetInt("HighestLevel", gameLevel);
    }

    // Handle enemy defeat and spawn the next enemy, boss or level
    public void EnemyDefeated()
    {
        remainingEnemyCount--;
        int monsterKill = PlayerPrefs.GetInt("MonsterKill", 0);
        monsterKill++;
        PlayerPrefs.SetInt("MonsterKill", monsterKill);

        // Spawn the next enemy if enemies remain
        if (remainingEnemyCount > 0)
        {
            enemySpawner.SpawnEnemy(gameLevel);
        }
        // Spawn boss when all enemies are defeated
        else if (!bossSpawned)
        {
            enemySpawner.SpawnBoss(gameLevel);
            bossSpawned = true;
            StartBossTimer();

        }
        // Level up and start the next wave after boss is defeated
        else if(bossSpawned)
        {
            GameLevelUp();
            RemainingEnemyCountReset();
            bossSpawned = false;
            enemySpawner.SpawnEnemy(gameLevel);
            int bossKill = PlayerPrefs.GetInt("BossKill", 0);
            bossKill++;
            PlayerPrefs.SetInt("BossKill", bossKill);
        }
    }

    // Start the boss level timer
    public void StartBossTimer()
    {
        bossTimerCoroutine = StartCoroutine(BossTimer());
    }

    // Stop the boss level timer
    public void StopBossTimer()
    {
        if(bossTimerCoroutine != null)
        {
            StopCoroutine(bossTimerCoroutine);
            bossTimerCoroutine = null;
        }
    }

    // Start boss timer and trigger level failure when time runs out
    public IEnumerator BossTimer()
    {
        float remainingTime = 30f;

        while(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        LevelFailed();
    }

    // Handle level failure and spawn next enemy
    public void LevelFailed()
    {
        isLevelFailed = true;
        OnLevelFailed?.Invoke();
        RemainingEnemyCountReset();
        bossSpawned = false;

        // Prevent spawning when timer stopped or paused and boss is defeated
        if (isGameRunning && isLevelFailed)
        {
            enemySpawner.SpawnEnemy(gameLevel);
            isLevelFailed = false;
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
    public void PauseGame(bool paused)
    {
        if (paused && isGameRunning)
        {
            isGameRunning = false;
            countdownTimer.PauseTimer(); // Pause the timer
            playerUI.StopRunningAnimation();
        }
        else if(!paused && !isGameRunning)
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
