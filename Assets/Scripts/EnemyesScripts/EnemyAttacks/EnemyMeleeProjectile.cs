using System.Collections;
using UnityEngine;

public class EnemyMeleeProjectile : MeleeEnemy
{
    [Header("SpecialAttackSettings")]

    [SerializeField] protected float specialAttackDistance = 10;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform projectileTransform;

    private Transform playerTransform;
    private EnemyGroundedChasingPatrol enemyChasingPatrol;

    private new void Start()
    {
        base.Start();
        enemyChasingPatrol = GetComponent<EnemyGroundedChasingPatrol>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private new void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, specialAttackDistance, playerLayer) == true && Vector2.Distance(transform.position, playerTransform.position) >= enemyChasingPatrol.aggroDistance)
        {
            ProjectileAttack();
        }
        else
        {
            Attack();
        }
    }

    private void ProjectileAttack()
    {
        animator.SetTrigger("specialAttack");
    }

    private void ApplyProjectileAttack()
    {
        Instantiate(projectilePrefab, projectileTransform.position,Quaternion.identity);
        soundPlay.PlaySound(1);
    }

    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, specialAttackDistance);
    }
}
