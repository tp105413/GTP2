using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private Vector3 spawnPosition = new Vector3(10f, -2f, 0f);

    public void SpawnEnemy(int level)
    {
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        // enemy prefab not in the scene before instantiate
        enemy.Initialize(playerCombat, level, "Enemy");
    }

    public void SpawnBoss(int level)
    {
        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.Initialize(playerCombat, level, "Boss");
    }
}
