using UnityEngine;

public class StandartBullet : Bullet
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyHealth enemy)) // Проверяем, является ли объект врагом
        {
            enemy.TakeDamage(damage);
        }
        if (collision.TryGetComponent(out BossHealth boss)) // Проверяем, является ли объект врагом
        {
            boss.GetDamage(damage);
        }
        Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject); // Уничтожаем пулю при столкновении
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
