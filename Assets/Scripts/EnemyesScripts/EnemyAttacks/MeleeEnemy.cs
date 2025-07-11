using UnityEngine;
using System.Collections;

public class MeleeEnemy : EnemyAttackType
{
    [Header("Melee attack settings")]
    [SerializeField] protected Vector3 attackPosOffset;
    [SerializeField] protected float attackRadius = 1.5f; // ������ �����
    [SerializeField] protected LayerMask playerLayer; // ���� ������ ��� ��������

    [Header("Effects")]
    [SerializeField] private ParticleSystem attackEffect;

    protected bool canAttack = true; // �������� ���������� Attack
    protected SoundPlay soundPlay;
    protected GameObject targetPlayer; // ���������� ������ �� ������

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
        if (!canAttack) return; // ���� ����� ���������, ������ ������� �� ������

        // ���������� �������� ������� ����� � ������ ����������� �����
        Vector3 adjustedAttackOffset = attackPosOffset;
        adjustedAttackOffset.x *= Mathf.Sign(transform.localScale.x); // ������ ����������� ��������

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
            // ���������, ��� ��� �� ���� � �������
            Collider2D playerStillInRange = Physics2D.OverlapCircle(transform.position + adjustedAttackOffset, attackRadius, playerLayer);
            if (playerStillInRange == null || playerStillInRange.gameObject != targetPlayer)
            {
                ResetAttackTarget();
            }
        }
    }

    // ���������� �� ������ ����� ����� (Animation Event)
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
        // ������������ ������� ����� � ���������
        Vector3 adjustedAttackOffset = attackPosOffset;
        if (Application.isPlaying) // ������ � ������ ����
        {
            adjustedAttackOffset.x *= Mathf.Sign(transform.localScale.x);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + adjustedAttackOffset, attackRadius);
    }

    // ����� ���� ����� ���������� ����� (Animation Event)
    protected void ResetAttackTarget()
    {
        targetPlayer = null;
    }
}
