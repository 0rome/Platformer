using UnityEngine;

public class GrenadeThrower : EnemyWeapon
{
    [Header("Settings")]
    [SerializeField] private float findRadius;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwCooldown = 2f;
    [SerializeField] private float throwForce = 10f;   // 👈 сила броска
    [SerializeField] private float throwAngle = 0.3f;  // 👈 угол навеса (чем больше, тем выше дуга)

    private Transform player;
    private float cooldownTimer;
    private SoundPlay soundPlay;


    private void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        cooldownTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        // кидаем только если игрок в радиусе
        if (distance < findRadius && cooldownTimer <= 0f)
        {
            ThrowProjectile();
            cooldownTimer = throwCooldown;
        }
    }

    private void ThrowProjectile()
    {
        soundPlay.PlaySound(0);

       GameObject obj = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (player.position - throwPoint.position).normalized;

            // добавляем "навес" вверх
            direction.y += throwAngle;

            // нормализуем, чтобы не было слишком разных скоростей
            direction.Normalize();

            rb.linearVelocity = direction * throwForce;
        }
    }
}
