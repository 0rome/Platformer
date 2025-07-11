using UnityEngine;

public class RicochetBullet : Bullet
{
    public override void BulletMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Двигаем пулю вперёд
    }
    private void Update()
    {
        BulletMovement();
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
