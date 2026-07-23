using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gameLevel = 1;

    public EnemySpawner enemySpawner;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        
    }

    private void StartTimer()
    {
        enemySpawner.SpawnEnemy(gameLevel);
    }

    public void GameLevelUp()
    {
        gameLevel++;
        Debug.Log("Game Level: " + gameLevel);
        enemySpawner.SpawnEnemy(gameLevel);
    }
}
