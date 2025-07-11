using UnityEngine;
using UnityEngine.UIElements;

public class EnemyGroundedPatrolling : EnemyMovement
{
    [Header("Patrol Settings")]
    public float waitTime = 1f; // Время ожидания на точке
    public float speed = 3f; // Скорость передвижения
    public float patrolDistance = 5;

    [Header("Jump Settings")]
    public float jumpForce = 10f; // Сила прыжка
    public float wallCheckDistance = 0.6f; // Расстояние до стены
    public float groundCheckRadius = 0.2f; // Радиус проверки земли
    public Transform groundCheck; // Точка проверки земли
    public Transform wallCheck; // Точка проверки стен
    public LayerMask groundLayer; // Слой земли

    protected Rigidbody2D rb;
    protected bool isGrounded;
    protected bool wallAhead;
    protected bool movingRight = true;

    private Vector3 pointA;
    private Vector3 pointB;
    private float waitTimer; // Таймер ожидания
    private Vector3 targetPoint;

    protected SoundPlay soundPlay;

    protected void Start()
    {
        pointA = transform.position;
        pointB = pointA + transform.right * patrolDistance;
        targetPoint = pointA; // Начинаем движение к первой точке
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        soundPlay = transform.Find("Sounds").GetComponent<SoundPlay>();
    }

    void Update()
    {
        //wallAhead = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallCheckDistance, groundLayer);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        wallAhead = Physics2D.OverlapCircle(wallCheck.position, wallCheckDistance, groundLayer);

        Debug.Log("isGrounded: " + isGrounded);

        Patrol();
    }

    public override void Movement()
    {
        Patrol();
    }
    protected void Patrol()
    {
        Jump();

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            animator.SetFloat("speed", 0);

            if (waitTimer <= 0f)
            {
                animator.SetFloat("speed", 1);
                targetPoint = (targetPoint == pointA) ? pointB : pointA; // Меняем цель на противоположную точку
            }
        }
        else
        {
            
            // Двигаем врага к целевой точке
            transform.position = Vector3.MoveTowards(transform.position,targetPoint,Time.deltaTime * speed);

            // Рассчитываем направление движения
            Vector3 direction = (targetPoint - transform.position).normalized;

            // Если есть движение, обновляем направление (вправо или влево)
            if (direction.x > 0.1f) // Движение вправо
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < -0.1f) // Движение влево
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Проверяем, достиг ли враг целевой точки
            if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
            {
                waitTimer = waitTime; // Устанавливаем таймер ожидания
            }
        }
    }
    protected void Jump()
    {
        if (isGrounded && wallAhead)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            // Показываем точки патрулирования в режиме редактирования
            Vector3 currentPosition = transform.position;
            Vector3 previewPointB = currentPosition + transform.right * patrolDistance;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(currentPosition, previewPointB);
            Gizmos.DrawSphere(currentPosition, 0.1f);
            Gizmos.DrawSphere(previewPointB, 0.1f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA, pointB);
            Gizmos.DrawSphere(pointA, 0.1f);
            Gizmos.DrawSphere(pointB, 0.1f);
        }
    }
}
