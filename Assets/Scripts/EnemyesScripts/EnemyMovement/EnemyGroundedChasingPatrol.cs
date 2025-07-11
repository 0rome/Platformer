using UnityEngine;

public class EnemyGroundedChasingPatrol : EnemyGroundedPatrolling
{
    [Header("Chasing Settings")]
    public float chasingSpeed = 3f; // Скорость передвижения
    public float aggroDistance = 5f; // Радиус преследования

    private Transform player;
    private Vector3 lastPosition;

    new void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        wallAhead = Physics2D.OverlapCircle(wallCheck.position, wallCheckDistance, groundLayer);

        if (wallAhead)
        {
            Jump();
        }
        else if (player != null)
        {
            Chasing();
        }
        else
        {
            Movement();
        }
    }

    private void Chasing()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Проверяем, нет ли препятствий на пути к игроку
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, groundLayer);

        // Если игрок в поле зрения и нет препятствий
        if (distanceToPlayer <= aggroDistance && hit.collider == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), Time.deltaTime * chasingSpeed);

            // Обновляем направление взгляда (вправо/влево)
            if (directionToPlayer.x > 0.1f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (directionToPlayer.x < -0.1f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
            float speed = velocity.magnitude;
            lastPosition = transform.position;

            animator.SetFloat("speed", speed > 0 ? 1 : 0);
        }
        else
        {
            Movement(); // Обычное патрулирование
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, aggroRadius);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + aggroDistance,transform.position.y));

        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
