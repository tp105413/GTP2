using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerUI playerUI;

    private Enemy attackTarget;
    private float attackTimer = 0f;
    private bool isCritical = false;


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
        int damage = CalculateDamage();
        attackTarget.TakeDamage(damage, playerStats);
        playerUI.PlayAttackTrigger();

        // ======critical damage ui apply here======
    }

    // Calculate final damage dealt by the player
    public int CalculateDamage()
    {
        float finalDamage = playerStats.AttackDamage;

        // Reset critical state
        isCritical = false;

        if (Random.value <= (playerStats.CriticalChance) / 100f)
        {
            isCritical = true;
            finalDamage *= 2 + (playerStats.CriticalDamage / 100f);
        }

        return Mathf.RoundToInt(finalDamage);
    }
}
