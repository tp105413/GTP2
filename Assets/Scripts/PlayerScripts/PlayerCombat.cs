using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerSkillManager playerSkillManager;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private UIManager uIManager;

    [SerializeField] private RectTransform containerParent;
    [SerializeField] private Image powerStrikePrefab;

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

        if (isCritical)
        {
            uIManager.SpawnDamagePopUpUI(damage, true, false, attackTarget);
        }
        else
        {
            uIManager.SpawnDamagePopUpUI(damage, false, false, attackTarget);
        }

        if(attackTarget != null && !attackTarget.isDead)
        {
            CheckSkills();
        }


        int highestDamage = PlayerPrefs.GetInt("HighestAttackDamage", 0);
        // For Achievement Record ( CritDMG is The highest ForNow)
        if (damage > highestDamage)
        {
            PlayerPrefs.SetInt("HighestAttackDamage", damage);
        }

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

    // Check the skills player can use in combat
    public void CheckSkills()
    {
        if (playerSkillManager.CheckPowerStrike())
        {
            StartCoroutine(PowerStrikeRoutine(attackTarget));
        }
    }

    // Wait for Power Strike visual effect execute completely
    IEnumerator PowerStrikeRoutine(Enemy target)
    {
        if(powerStrikePrefab != null && target != null)
        {
            Image fx = Instantiate(powerStrikePrefab, target.transform.position, Quaternion.identity, containerParent);
            Destroy(fx.gameObject, 0.5f);
        }

        yield return new WaitForSeconds(0.3f);

        // If enemy exists and not dead, deals skill damage to enemy
        if(target != null && !target.isDead)
        {
            int damage = playerSkillManager.GetPowerStrikeDamage();
            target.TakeDamage(damage, playerStats);
            uIManager.SpawnDamagePopUpUI(damage, false, true, target);
        }
    }
}
