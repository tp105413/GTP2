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
        // enemy prefab not in the scene before instantiate
        enemy.Initialize(gameManager, level, "Enemy");
        playerCombat.SetTarget(enemy);
    }

    public void SpawnBoss(int level)
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.Initialize(gameManager, level, "Boss");
        playerCombat.SetTarget(enemy);
    }
}
