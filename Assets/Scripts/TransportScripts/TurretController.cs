using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : Transport
{
    [Header("��������� ������")]

    [SerializeField] private Transform firePoint; // ����� ��������
    [SerializeField] private GameObject bulletPrefab; // ������ �������
    [SerializeField] private float bulletSpeed = 10f; // �������� �������
    [SerializeField] private float angleOffset = 0f; // ��������� ����

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
        // �������� ������� ������� ���� � ������������ ������
        Vector3 mousePosition = Input.mousePosition;

        // ����������� ������� ������� � ���������� ����
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0; // ������������� Z � 0 ��� 2D

        // ������������ ����������� �� ������ � �������
        Vector3 direction = worldPosition - transform.position;

        // ��������� ���� ����� ������� � ��������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ��������� ���� ��������
        angle += angleOffset;

        // ������������� ���� �������� ������
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)) // ����� ������ ����
        {
            animator.SetTrigger("Fire");

            // ������� ������ � ����� firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

            // ������ �������� �������
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * bulletSpeed;
            }
        }
    }
}
