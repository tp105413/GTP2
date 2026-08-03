using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerStats playerStats;

    private Enemy attackTarget;
    private float timer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= playerStats.attackInterval && attackTarget != null)
        {
            Attack();
            timer = 0f;
        }
    }

    public void SetTarget(Enemy enemy)
    {
        attackTarget = enemy;
    }

    public void Attack()
    {
        attackTarget.TakeDamage(playerStats.attackDamage, playerStats);
    }
}
