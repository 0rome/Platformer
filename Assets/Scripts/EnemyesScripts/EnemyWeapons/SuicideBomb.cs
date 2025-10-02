using UnityEngine;

public class SuicideBomb : EnemyWeapon
{
    [SerializeField] private float activationRadius = 3f;
    [SerializeField] private GameObject explosionPrefab;
    
    // Update is called once per frame
    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, activationRadius);

        if (hit != null && hit.CompareTag("Player"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
