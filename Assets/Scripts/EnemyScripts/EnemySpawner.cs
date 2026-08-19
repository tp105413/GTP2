using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private RectTransform containerParent;
    [SerializeField] private Vector2 spawnPosition = new Vector2(0f, 0f);

    public void SpawnEnemy(int level) => Spawn(level, "Enemy");

    public void SpawnBoss(int level) => Spawn(level, "Boss");

    private void Spawn(int level, string type)
    {
        Enemy enemy = Instantiate(enemyPrefab, containerParent);
        EnemyUI ui = enemy.GetComponent<EnemyUI>();
        ui.SetSpawnPosition(spawnPosition);
        enemy.Initialize(playerCombat, countdownTimer, level, type);
    }
}
