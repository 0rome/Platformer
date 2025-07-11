using UnityEngine;
using System.Collections;

public class MeleeEnemy : EnemyAttackType
{
    [Header("Melee attack settings")]
    [SerializeField] protected Vector3 attackPosOffset;
    [SerializeField] protected float attackRadius = 1.5f; // Радиус атаки
    [SerializeField] protected LayerMask playerLayer; // Слой игрока для проверки

    [Header("Effects")]
    [SerializeField] private ParticleSystem attackEffect;

    protected bool canAttack = true; // Контроль выполнения Attack
    protected SoundPlay soundPlay;
    protected GameObject targetPlayer; // Сохранение ссылки на игрока

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();

        soundPlay = transform.Find("Sounds").GetComponent<SoundPlay>();
    }

    protected virtual void Update()
    {
        Attack();
    }

    public override void Attack()
    {
        if (!canAttack) return; // Если атака запрещена, просто выходим из метода

        // Определяем смещение радиуса атаки с учетом направления врага
        Vector3 adjustedAttackOffset = attackPosOffset;
        adjustedAttackOffset.x *= Mathf.Sign(transform.localScale.x); // Меняем направление смещения

        if (targetPlayer == null)
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position + adjustedAttackOffset, attackRadius, playerLayer);

            if (player != null)
            {
                targetPlayer = player.gameObject;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            // Проверяем, все еще ли цель в радиусе
            Collider2D playerStillInRange = Physics2D.OverlapCircle(transform.position + adjustedAttackOffset, attackRadius, playerLayer);
            if (playerStillInRange == null || playerStillInRange.gameObject != targetPlayer)
            {
                ResetAttackTarget();
            }
        }
    }

    // Вызывается на первом кадре атаки (Animation Event)
    private void ApplyAttack()
    {
        if (attackEffect != null)
        {
            attackEffect.Play();
            soundPlay.PlaySound(0);
        }

        Collider2D playerStillInRange = Physics2D.OverlapCircle(transform.position + attackPosOffset, attackRadius, playerLayer);
        if (playerStillInRange != null && playerStillInRange.gameObject == targetPlayer)
        {
            PlayerHealth playerHealth = targetPlayer.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Death();
            }
        }
    }

    protected void OnDrawGizmosSelected()
    {
        // Визуализация радиуса атаки в редакторе
        Vector3 adjustedAttackOffset = attackPosOffset;
        if (Application.isPlaying) // Только в режиме игры
        {
            adjustedAttackOffset.x *= Mathf.Sign(transform.localScale.x);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + adjustedAttackOffset, attackRadius);
    }

    // Сброс цели после завершения атаки (Animation Event)
    protected void ResetAttackTarget()
    {
        targetPlayer = null;
    }
}
