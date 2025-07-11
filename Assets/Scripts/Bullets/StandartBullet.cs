using UnityEngine;

public class StandartBullet : Bullet
{
    public override void BulletMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // Двигаем пулю вперёд
    }
    // Update is called once per frame
    private void Update()
    {
        BulletMovement();
    }
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
}
