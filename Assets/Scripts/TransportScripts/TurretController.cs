using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : Transport
{
    [Header("Настройки турели")]

    [SerializeField] private Transform firePoint; // Точка выстрела
    [SerializeField] private GameObject bulletPrefab; // Префаб снаряда
    [SerializeField] private float bulletSpeed = 10f; // Скорость снаряда
    [SerializeField] private float angleOffset = 0f; // Коррекция угла

    private Animator animator;
    private Camera mainCamera;

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Controll();
        Shoot();
    }

    public override void Controll()
    {
        // Получаем позицию курсора мыши в пространстве экрана
        Vector3 mousePosition = Input.mousePosition;

        // Преобразуем позицию курсора в координаты мира
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0; // Устанавливаем Z в 0 для 2D

        // Рассчитываем направление от турели к курсору
        Vector3 direction = worldPosition - transform.position;

        // Вычисляем угол между турелью и курсором
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Учитываем угол смещения
        angle += angleOffset;

        // Устанавливаем угол вращения турели
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            animator.SetTrigger("Fire");

            // Создаем снаряд в точке firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

            // Задаем скорость снаряду
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * bulletSpeed;
            }
        }
    }
}
