using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float lifeTime = 2f; // ¬рем€ жизни пули
    [SerializeField] protected GameObject DestroyEffect;

    protected int damage;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // ”ничтожаем пулю через заданное врем€
    }
    public virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
