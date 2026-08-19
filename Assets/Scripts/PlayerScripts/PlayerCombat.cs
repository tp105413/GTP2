using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerUI playerUI;

    private Enemy attackTarget;
    private float attackTimer = 0f;

    void Update()
    {
        // Track attack cooldown based on frame time
        attackTimer += Time.deltaTime;

        if(attackTimer >= playerStats.AttackInterval && attackTarget != null)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    // Set enemy target for player
    public void SetTarget(Enemy enemy)
    {
        attackTarget = enemy;
    }

    // Deal damage and trigger attack animation
    public void Attack()
    {
        attackTarget.TakeDamage(playerStats.AttackDamage, playerStats);
        playerUI.PlayAttackTrigger();
    }
}
