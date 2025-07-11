using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float speed = 20f; // Скорость пули
    [SerializeField] protected float lifeTime = 2f; // Время жизни пули
    [SerializeField] protected GameObject DestroyEffect;

    protected int damage;

    public abstract void BulletMovement();

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Уничтожаем пулю через заданное время
    }
    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
