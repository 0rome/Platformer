using Unity.Cinemachine;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("Homing missle settings")]
    [SerializeField] private float speed = 10f; // �������� ������
    [SerializeField] private float rotationSpeed = 200f; // �������� ��������
    [SerializeField] private float lifeTime = 5f; // ����� ����� �������
    [SerializeField] private GameObject deathEffect;

    private Transform target; // ���� (��������, �����)

    private void Start()
    {
        // ����� ������ ��� ������
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;

            // ��������� ����������� �� ����
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ��������� ������ � ������� ����
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // ���������� ������ ����� �������� �����
        Invoke("DestroyObject", lifeTime);
    }

    private void Update()
    {
        if (target != null)
        {
            // ��������� ������ � ����
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            // ���������� ���� �������� � ����
            float rotateAmount = Vector3.Cross(direction, transform.right).z;

            // ������� �������
            transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);
        }
        if (target.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
        {
            DestroyObject();
        }
        

        // ��������� ������
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().Death();
        }
        DestroyObject();
    }

    private void DestroyObject()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // ������������ ������� �������� (���� �����������)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
