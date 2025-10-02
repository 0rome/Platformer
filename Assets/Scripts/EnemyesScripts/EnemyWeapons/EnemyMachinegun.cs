using UnityEngine;

public class EnemyMachinegun : EnemyWeaponAim
{
    [Header("Shooting")]
    [SerializeField] private float spreadAngle = 5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    [Header("Fire Settings")]
    [SerializeField] private float fireRate = 0.2f; // время между выстрелами (секунды)
    private float nextFireTime = 0f;

    

    protected override void Fire(Vector2 targetPos)
    {
        if (Time.time < nextFireTime) return; // еще не настало время стрелять
        nextFireTime = Time.time + fireRate;

        if (projectilePrefab == null || firePoint == null) return;

        // Рассчитываем направление
        Vector2 dir = (targetPos - (Vector2)firePoint.position).normalized;

        // Добавляем разброс
        float randomAngle = Random.Range(-spreadAngle, spreadAngle); // угол разброса в градусах
        dir = Quaternion.Euler(0, 0, randomAngle) * dir;

        // Создаём пулю
        var go = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir * projectileSpeed;

        shootEffect.Play();
        soundPlay.PlaySound(0);
        Destroy(go, 3f);
    }
}
