using System.Collections;
using UnityEngine;

public class SpaceshipBossController : MonoBehaviour
{
    [Header("AttackSettings")]
    [SerializeField] private Transform gun1_point;
    [SerializeField] private Transform gun2_point;
    [SerializeField] private Transform laser1_point;
    [SerializeField] private Transform laser2_point;

    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private GameObject[] projectiles;

    [Header("Laser Settings")]
    [SerializeField] private float laserRange = 12f;
    [SerializeField] private float laserWidth = 0.4f;
    [SerializeField] private float warningDuration = 1.2f;
    [SerializeField] private float laserDuration = 1.0f;
    [SerializeField] private LayerMask damageMask;

    [Header("Laser Visuals")]
    [SerializeField] private LineRenderer laserLinePrefab; // Prefab с LineRenderer

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackGun_1()
    {
        animator.SetTrigger("Gun_2");

        Vector2 dir = gun1_point.up;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        var currentProjectile = Instantiate(
            projectiles[0],
            gun1_point.position,
            Quaternion.Euler(0, 0, 0)
        );

        var rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir.normalized * projectileSpeed;
    }

    public void AttackGun_2()
    {
        animator.SetTrigger("Gun_1");

        Vector2 dir = gun2_point.up;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        var currentProjectile = Instantiate(
            projectiles[0],
            gun2_point.position,
            Quaternion.Euler(0, 0, 0)
        );

        var rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir.normalized * projectileSpeed;
    }
    public void AttackGunsBoth()
    {
        animator.SetTrigger("BothGuns");

        AttackGun_1();
        AttackGun_2();
    }
    public void AttackLaser_1()
    {
        animator.SetTrigger("Laser_1");
        FireLaser(laser1_point);
        
    }

    public void AttackLaser_2()
    {
        animator.SetTrigger("Laser_2");
        FireLaser(laser2_point);
    }
    public void AttackLasersBoth()
    {
        animator.SetTrigger("BothLasers");
        FireLaser(laser1_point);
        FireLaser(laser2_point);
    }
    // Лазерная атака из указанной пушки (laserPoint)
    private void FireLaser(Transform laserPoint)
    {
        StartCoroutine(FireLaserRoutine(laserPoint));
    }

    private IEnumerator FireLaserRoutine(Transform laserPoint)
    {
        LineRenderer lr = Instantiate(laserLinePrefab);
        lr.positionCount = 2;
        lr.enabled = true;

        float warningWidth = 0.06f;
        Color warningColor = new Color(1f, 0.25f, 0.25f, 0.8f);
        Color laserColor = new Color(1f, 0.9f, 0.2f, 1f);

        // WARNING PHASE
        float t = 0f;
        while (t < warningDuration)
        {
            t += Time.deltaTime;

            Vector2 dir = laserPoint.up;
            Vector3 start = laserPoint.position;
            Vector3 end = start + (Vector3)(dir.normalized * laserRange);

            // Пульсация warning (альфа)
            float alpha = Mathf.PingPong(Time.time * 2f, 0.5f) + 0.5f;
            Color c = warningColor; c.a *= alpha;

            lr.startColor = lr.endColor = c;
            lr.startWidth = lr.endWidth = warningWidth;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            yield return null;
        }

        // LASER PHASE
        float elapsed = 0f;
        while (elapsed < laserDuration)
        {
            elapsed += Time.deltaTime;

            Vector2 dir = laserPoint.up;
            Vector3 start = laserPoint.position;
            Vector3 end = start + (Vector3)(dir.normalized * laserRange);

            // Пульсируем визуал
            float widthPulse = laserWidth * (0.8f + 0.2f * Mathf.Sin(Time.time * 10f));
            lr.startWidth = lr.endWidth = widthPulse;
            lr.startColor = lr.endColor = laserColor;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            // Урон по зоне
            Vector2 origin = (Vector2)laserPoint.position + dir.normalized * (laserRange * 0.5f);

            Vector2 size = new Vector2(laserRange, laserWidth);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Collider2D[] hits = Physics2D.OverlapBoxAll(origin, size, angle, damageMask);
            foreach (var c in hits)
            {
                var dmg = c.GetComponent<SpaceshipHealth>();
                if (dmg != null)
                    dmg.DestroySpaceship(); // или dmg.TakeDamage(amount);
            }

            yield return null;
        }


        // SHRINK PHASE: laser исчезает к пушке
        float shrinkTime = 0.3f;
        float shrinkT = 0f;
        Vector3 laserStart = laserPoint.position;
        Vector3 laserEnd = laserStart + (Vector3)(laserPoint.up.normalized * laserRange);

        while (shrinkT < shrinkTime)
        {
            shrinkT += Time.deltaTime;
            float f = 1f - shrinkT / shrinkTime; // 1 → 0
            Vector3 currentEnd = Vector3.Lerp(laserStart, laserEnd, f);
            lr.SetPosition(0, laserStart);
            lr.SetPosition(1, currentEnd);
            yield return null;
        }

        Destroy(lr.gameObject);
    }

}
