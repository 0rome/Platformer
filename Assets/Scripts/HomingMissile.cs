using Unity.Cinemachine;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("Homing missle settings")]
    [SerializeField] private float speed = 10f; // Скорость полета
    [SerializeField] private float rotationSpeed = 200f; // Скорость поворота
    [SerializeField] private float lifeTime = 5f; // Время жизни снаряда
    [SerializeField] private GameObject deathEffect;

    private Transform target; // Цель (например, игрок)

    private void Start()
    {
        // Найти игрока при старте
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;

            // Вычислить направление на цель
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Повернуть снаряд в сторону цели
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Уничтожить объект через заданное время
        Invoke("DestroyObject", lifeTime);
    }

    private void Update()
    {
        if (target != null)
        {
            // Направить снаряд к цели
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            // Рассчитать угол поворота к цели
            float rotateAmount = Vector3.Cross(direction, transform.right).z;

            // Поворот снаряда
            transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);
        }
        if (target.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
        {
            DestroyObject();
        }
        

        // Двигаться вперед
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
        // Визуализация радиуса действия (если понадобится)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
