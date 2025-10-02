using UnityEngine;

public class RangeEnemy : EnemyAttackType
{
    [SerializeField] private ParticleSystem shotEffect;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float laserLength = 5f;
    [SerializeField] private LayerMask playerLayer;

    private Vector2 direction;


    protected override void Start()
    {
        base.Start();
    }
    protected virtual void Update()
    {
        FindPlayer();
    }

    public override void Attack()
    {
        soundPlay.PlaySound(0);

        shotEffect.Play();

        Quaternion rotation = transform.localScale.x > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

        Instantiate(projectilePrefab, spawnPoint.position, rotation);
    }
    private void FindPlayer()
    {
        if (transform.localScale.x > 0)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, laserLength, playerLayer);

        Debug.DrawRay(transform.position, direction.normalized * laserLength, Color.red); // Видимый луч в редакторе

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {

                animator.SetTrigger("Attack");
            }
        }
    }
}
