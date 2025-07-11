using UnityEngine;

public class Mine : Traps
{
    [SerializeField] private GameObject explosionPrefab;

    private new void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Explosion();
        }

        if (collision.GetComponent<EnemyHealth>())
        {
            Explosion();
        }

        if (collision.GetComponent<Bullet>())
        {
            Explosion();
        }

        if (collision.GetComponent<DeathZone>())
        {
            Explosion();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
        {
            Explosion();
        }
    }
    private void Explosion()
    {
        CameraShake.instance.Shake(0.3f, 0.5f);
        Instantiate(explosionPrefab,transform.position,Quaternion.identity);
        gameObject.SetActive(false);
    }
}
