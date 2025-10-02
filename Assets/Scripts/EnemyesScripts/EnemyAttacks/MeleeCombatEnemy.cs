using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeCombatEnemy : EnemyAttackType
{
    [Header("Settings")]
    [SerializeField] private List<AttackZone> attackZones = new List<AttackZone>();
    [SerializeField] protected LayerMask playerLayer;

    [Header("Effects")]
    [SerializeField] protected GameObject attackEffects;

    [SerializeField] private float attackCooldown = 1f;


    protected bool canAttack = true;
    protected GameObject targetPlayer;

    protected override void Start()
    {
        base.Start();
    }

    protected virtual void Update()
    {
        if (canAttack)
            Attack();
    }

    //Атака
    public override void Attack()
    {
        if (attackZones.Count == 0) return;

        Vector3 adjustedOffset = GetAdjustedOffset(attackZones[0].offset);

        if (targetPlayer == null)
        {
            Collider2D player = Physics2D.OverlapBox(transform.position + adjustedOffset, attackZones[0].radius, 0f, playerLayer);

            if (player != null)
            {
                targetPlayer = player.gameObject;
                animator.SetTrigger("Attack");
                StartCoroutine(AttackCooldown());
            }
        }
        else
        {
            Collider2D playerStillInRange = Physics2D.OverlapBox(transform.position + adjustedOffset, attackZones[0].radius, 0f, playerLayer);
            if (playerStillInRange == null || playerStillInRange.gameObject != targetPlayer)
            {
                ResetAttackTarget();
            }
        }
    }

    //Смещение радиуса атаки
    private Vector3 GetAdjustedOffset(Vector3 offset)
    {
        Vector3 adjustedOffset = offset;
        adjustedOffset.x *= Mathf.Sign(transform.localScale.x);
        return adjustedOffset;
    }


    //Проверка нахождения игрока в зоне атаки
    private bool IsPlayerInZone(AttackZone zone)
    {
        Vector3 adjustedOffset = GetAdjustedOffset(zone.offset);
        Collider2D hit = Physics2D.OverlapBox(transform.position + adjustedOffset, zone.radius, 0f, playerLayer);
        return hit != null && hit.gameObject == targetPlayer;
    }


    //Вызывается при подтверждении атаки в аниматоре на кадре атаки
    public void ApplyAttack(int attackIndex)
    {
        if (attackIndex < 0 || attackIndex >= attackZones.Count)
            return;

        // Проигрываем эффект
        if (attackEffects != null)
        {
            attackEffects.transform.GetChild(attackIndex).GetComponent<ParticleSystem>().Play();
            soundPlay.PlaySound(attackIndex);
        }

        // Смещение с учётом направления
        Vector3 adjustedOffset = GetAdjustedOffset(attackZones[attackIndex].offset);

        Collider2D hit = Physics2D.OverlapBox(transform.position + adjustedOffset, attackZones[attackIndex].radius, 0f, playerLayer);
        if (hit != null && hit.gameObject == targetPlayer)
        {
            PlayerHealth health = targetPlayer.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Death();
            }
        }
        RaiseAttackEvent();
    }


    //Перезарядка атаки
    private System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    // Сброс цели после завершения атаки (Animation Event)
    protected void ResetAttackTarget()
    {
        targetPlayer = null;
    }


    protected void OnDrawGizmosSelected()
    {
        if (attackZones == null || attackZones.Count == 0)
            return;

        Gizmos.color = Color.red;
        Vector3 offset0 = Application.isPlaying ? GetAdjustedOffset(attackZones[0].offset) : attackZones[0].offset;
        Gizmos.DrawWireCube(transform.position + offset0, attackZones[0].radius);

        if (attackZones.Count > 1)
        {
            Gizmos.color = Color.yellow;
            Vector3 offset1 = Application.isPlaying ? GetAdjustedOffset(attackZones[1].offset) : attackZones[1].offset;
            Gizmos.DrawWireCube(transform.position + offset1, attackZones[1].radius);
        }
    }
}
