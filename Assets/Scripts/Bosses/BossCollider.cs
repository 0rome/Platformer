using UnityEngine;

public class BossCollider : MonoBehaviour
{
    [SerializeField] private float damageModifier = 1;

    
    public void GetDamage(float damage)
    {
        transform.parent.parent.GetComponent<BossHealth>()?.GetDamage(damage * damageModifier);
        Debug.Log("Boss Damage");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerHealth>().Death();
        }
    }
}
