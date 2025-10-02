using UnityEngine;

public abstract class EnemyWeaponAim : EnemyWeapon
{
    [Header("Targeting")]
    [SerializeField] private float detectionRadius = 6f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private bool requireTagPlayer = true;

    [Header("Aiming")]
    [SerializeField] protected Transform turretHead;
    [SerializeField] private float rotationSpeed = 720f;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem shootEffect;

    protected Transform currentTarget;
    protected bool targetVisible;
    protected SoundPlay soundPlay;


    private void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
    }

    protected virtual void Update()
    {
        FindTarget();

        if (currentTarget != null && targetVisible)
        {
            AimAt(currentTarget.position);
            Fire(currentTarget.position); // стрельба
        }
        else
        {
            StopFire(); // <--- выключаем эффекты/звук, если никого нет
        }
    }

    protected void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        Transform best = null;
        float bestDist = float.MaxValue;
        targetVisible = false;

        foreach (var hit in hits)
        {
            if (hit == null) continue;
            if (requireTagPlayer && !hit.CompareTag("Player")) continue;

            Vector2 dir = ((Vector2)hit.transform.position - (Vector2)transform.position).normalized;
            float dist = Vector2.Distance(transform.position, hit.transform.position);

            RaycastHit2D r = Physics2D.Raycast(transform.position, dir, dist, obstacleLayerMask | playerLayer);
            if (r.collider != null && r.collider == hit)
            {
                if (dist < bestDist)
                {
                    best = hit.transform;
                    bestDist = dist;
                    targetVisible = true;
                }
            }
        }

        currentTarget = best;
    }

    protected void AimAt(Vector2 worldPos)
    {
        if (turretHead == null) return;

        Vector2 dir = worldPos - (Vector2)turretHead.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Если родитель развернут по X (отражён)
        if (turretHead.lossyScale.x < 0)
        {
            shootEffect.transform.localRotation = Quaternion.Euler(0, -180, 0);
            angle = 180f - angle;
        }
        else
        {
            shootEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        turretHead.localRotation = Quaternion.RotateTowards(
            turretHead.localRotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// Абстрактный метод стрельбы — каждый враг реализует по-своему
    /// </summary>
    protected abstract void Fire(Vector2 targetPos);

    /// <summary>
    /// Метод для остановки эффектов (реализуется в наследниках)
    /// </summary>
    protected virtual void StopFire() { }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
