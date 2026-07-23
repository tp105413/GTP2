using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public PlayerCombat playerCombat;
    public GameManager gameManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnEnemy(int level)
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.SetGameManager(gameManager);
        enemy.InitializeHealth(level);
        playerCombat.SetTarget(enemy);
    }
}
