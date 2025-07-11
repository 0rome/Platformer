using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float speed = 20f; // �������� ����
    [SerializeField] protected float lifeTime = 2f; // ����� ����� ����
    [SerializeField] protected GameObject DestroyEffect;

    protected int damage;

    public abstract void BulletMovement();

    private void Start()
    {
        Destroy(gameObject, lifeTime); // ���������� ���� ����� �������� �����
    }
    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
