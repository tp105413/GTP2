using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private GameManager gameManager;

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
