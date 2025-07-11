using UnityEngine;

public class BossController : Boss
{
    [SerializeField] protected float attackCooldown = 3;
    protected float nextAttackTime = 0f;

    protected virtual void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            PerformAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    protected virtual void PerformAttack()
    {

    }
}
