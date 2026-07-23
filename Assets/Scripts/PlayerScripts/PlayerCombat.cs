using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage;
    public float attackInterval;
    public Enemy attackTarget;

    private float timer = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= attackInterval && attackTarget != null)
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
        attackTarget.TakeDamage(attackDamage);
    }
}
