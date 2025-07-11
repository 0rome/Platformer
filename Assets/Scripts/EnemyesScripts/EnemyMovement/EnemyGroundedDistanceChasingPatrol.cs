using UnityEngine;

public class EnemyGroundedDistanceChasingPatrol : EnemyGroundedPatrolling
{
    [Header("Chasing Settings")]
    public float chasingSpeed = 3f; // Скорость передвижения
    public float aggroDistance = 5f; // Радиус преследования

    [Tooltip("Желаемое минимальное расстояние до игрока")]
    public float desiredDistance = 2f; // Минимальная дистанция

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

        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, groundLayer);

        // Если игрок в радиусе агро и нет стены
        if (distanceToPlayer <= aggroDistance && hit.collider == null)
        {
            // Если враг слишком далеко — идём ближе
            if (distanceToPlayer > desiredDistance + 0.1f)
            {
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * chasingSpeed);
            }
            // Если враг слишком близко — можно добавить отступление (по желанию)
            else if (distanceToPlayer < desiredDistance - 0.1f)
            {
                Vector3 retreatPosition = transform.position - new Vector3(directionToPlayer.x, 0f, 0f);
                transform.position = Vector3.MoveTowards(transform.position, retreatPosition, Time.deltaTime * chasingSpeed);
            }
            // Иначе — стоим на месте

            // Обновляем направление взгляда
            if (directionToPlayer.x > 0.1f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (directionToPlayer.x < -0.1f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Анимация
            Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
            float speed = velocity.magnitude;
            lastPosition = transform.position;

            animator.SetFloat("speed", speed > 0 ? 1 : 0);
        }
        else
        {
            Movement(); // Патрулирование
        }
    }
}
