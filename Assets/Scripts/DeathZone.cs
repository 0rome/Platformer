using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().Death();
        }

        if (collision.GetComponent<EnemyHealth>())
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(100);
        }
    }
}
