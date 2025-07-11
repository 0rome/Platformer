using UnityEngine;

public class StandartBullet : Bullet
{
    public override void BulletMovement()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime); // ������� ���� �����
    }
    // Update is called once per frame
    private void Update()
    {
        BulletMovement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyHealth enemy)) // ���������, �������� �� ������ ������
        {
            enemy.TakeDamage(damage);
        }
        if (collision.TryGetComponent(out BossHealth boss)) // ���������, �������� �� ������ ������
        {
            boss.GetDamage(damage);
        }
        Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject); // ���������� ���� ��� ������������
    }
}
