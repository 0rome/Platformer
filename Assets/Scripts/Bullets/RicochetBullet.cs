using UnityEngine;

public class RicochetBullet : Bullet
{
    public override void BulletMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // ������� ���� �����
    }
    private void Update()
    {
        BulletMovement();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out EnemyHealth enemy)) // ���������, �������� �� ������ ������
        {
            enemy.TakeDamage(damage);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // ���������� ���� ��� ������������
        }
        if (collision.collider.TryGetComponent(out BossCollider boss)) // ���������, �������� �� ������ ������
        {
            boss.GetDamage(damage);
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // ���������� ���� ��� ������������
        }
    }
}
