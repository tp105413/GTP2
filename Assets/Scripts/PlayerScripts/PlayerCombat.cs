using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerUI playerUI;

    private Enemy attackTarget;
    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= playerStats.AttackInterval && attackTarget != null)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    public void SetTarget(Enemy enemy)
    {
        attackTarget = enemy;
    }

    public void Attack()
    {
        attackTarget.TakeDamage(playerStats.AttackDamage, playerStats);
        playerUI.PlayAttackTrigger();
    }
}
