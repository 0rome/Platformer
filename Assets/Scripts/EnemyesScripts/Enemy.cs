using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour
{
    protected Animator animator;

    public event Action OnAttack;
    public event Action OnDead;
    public event Action OnTakeDamage;

    private EnemyMovement enemyMovement;
    private EnemyAttackType enemyAttackType;
    private EnemyWeapon enemyWeapon;

    public virtual void Awake()
    {
        animator = GetComponent<Animator>(); // Добавьте это если нужно

        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttackType = GetComponent<EnemyAttackType>();
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();
    }

    public void DeactivateEnemy()
    {
        if (enemyMovement != null) enemyMovement.enabled = false;
        if (enemyAttackType != null) enemyAttackType.enabled = false;
        if (enemyWeapon != null) enemyWeapon.gameObject.SetActive(false);
    }
    public void ActivateEnemy()
    {
        if (enemyMovement != null) enemyMovement.enabled = true;
        if (enemyAttackType != null) enemyAttackType.enabled = true;
        if (enemyWeapon != null) enemyWeapon.gameObject.SetActive(true);
    }
    protected void RaiseAttackEvent() => OnAttack?.Invoke();
    protected void RaiseDeadEvent() => OnDead?.Invoke();
    protected void RaiseDamageEvent() => OnTakeDamage?.Invoke();
}