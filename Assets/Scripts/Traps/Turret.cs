using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Turret : Traps
{
    [Header("Targeting")]
    [SerializeField] private float detectionRadius = 6f;
    [SerializeField] private LayerMask playerLayer;        // ���� � �������
    [SerializeField] private LayerMask obstacleLayerMask;  // ���� ����������� (�����)
    [SerializeField] private bool requireTagPlayer = true; // ��������� �� ��� "Player"

    [Header("Aiming")]
    [SerializeField] private Transform turretHead; // �����, ������� ��������� (sprite, child)
    [SerializeField] private float rotationSpeed = 720f; // ������� � �������

    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireRate = 1f; // ��������� � �������
    [SerializeField] private bool onlyOnceWhenSpotted = false; // �������� ������ ��� ������ ����������� (�����������)
    [SerializeField] private ParticleSystem ShootEffect;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;
    [SerializeField] private Color gizmoColor = Color.cyan;

    private float fireCooldown = 0f;
    private Transform currentTarget;
    private bool targetVisible;
    private bool hasSpottedOnce;
    private Animator animator;
    private SoundPlay soundPlay;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        soundPlay = GetComponent<SoundPlay>();
    }

    void Update()
    {
        // ����� ����� ������������ (����� �������������� � ��������)
        FindTarget();

        // �������������� � ���� (���� ���� � �����)
        if (currentTarget != null && targetVisible)
        {
            AimAt(currentTarget.position);
            TryFireAt(currentTarget.position);
        }
        else
        {
            // ����� �������� ��������� "��������������" ��� ������� � �������� ����
        }

        if (fireCooldown > 0f) fireCooldown -= Time.deltaTime;
    }

    void FindTarget()
    {
        // ������� ��� ���������� ������� � �������
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        Transform best = null;
        float bestDist = float.MaxValue;
        targetVisible = false;

        foreach (var hit in hits)
        {
            if (hit == null) continue;
            if (requireTagPlayer && !hit.CompareTag("Player")) continue;

            Vector2 dir = ((Vector2)hit.transform.position - (Vector2)firePoint.position).normalized;
            float dist = Vector2.Distance(firePoint.position, hit.transform.position);

            // Raycast ����� ��������� ������ ��������� (�� ����� �����)
            RaycastHit2D r = Physics2D.Raycast(firePoint.position, dir, dist, obstacleLayerMask | playerLayer);
            if (r.collider != null)
            {
                // ���� ������ �������� ����������� �������� ����� => �����
                if (r.collider == hit)
                {
                    // �������� ���������� ��������
                    if (dist < bestDist)
                    {
                        best = hit.transform;
                        bestDist = dist;
                        targetVisible = true;
                    }
                }
                else
                {
                    // �������� ����������� � �� ����� ����� ����
                }
            }
        }

        currentTarget = best;

        if (currentTarget != null)
        {
            if (!hasSpottedOnce)
            {
                hasSpottedOnce = true;
            }
        }
        else
        {
            // ���� ���� �������� � ����� �������� hasSpottedOnce � ����������� �� ������
            // hasSpottedOnce = false;
        }
    }

    void AimAt(Vector2 worldPos)
    {
        if (turretHead == null) return;

        Vector2 dir = worldPos - (Vector2)turretHead.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, angle); // -90 ���� ������ "����" ������������; ������� ��� ���� ������
        turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    void TryFireAt(Vector2 worldPos)
    {
        if (onlyOnceWhenSpotted && hasSpottedOnce && fireCooldown <= 0f)
        {
            // ���� ����� �������� ������ ���� ��� ��� ������ �����������
            Fire(worldPos);
            hasSpottedOnce = true; // ��� ������������
            fireCooldown = 1f / fireRate;
            return;
        }

        if (fireCooldown <= 0f)
        {
            Fire(worldPos);
            fireCooldown = 1f / fireRate;
        }
    }

    void Fire(Vector2 worldPos)
    {
        if (projectilePrefab == null || firePoint == null) return;

        Vector2 dir = (worldPos - (Vector2)firePoint.position).normalized;
        // ������ � ����� ��������
        var go = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * projectileSpeed;
        }
        Destroy(go,4f);
        animator.SetTrigger("Fire");
        ShootEffect.Play();
        soundPlay.PlaySound(0);
        // ���� � ������� ����������� ������, ������ �� ������
    }

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(firePoint.position, 0.05f);
        }
    }
}
