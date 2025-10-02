using UnityEngine;

public class RicochetBullet : Bullet
{
    private void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out EnemyHealth enemy)) // Проверяем, является ли объект врагом
        {
            enemy.TakeDamage(damage);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Уничтожаем пулю при столкновении
        }
        if (collision.collider.TryGetComponent(out BossCollider boss)) // Проверяем, является ли объект врагом
        {
            boss.GetDamage(damage);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Уничтожаем пулю при столкновении
        }
    }
}
