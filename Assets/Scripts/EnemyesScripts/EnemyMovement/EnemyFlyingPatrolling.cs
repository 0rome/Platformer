using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyingPatrolling : EnemyMovement
{
    [Header("Patrol Settings")]
    [SerializeField] protected float patrolDistance = 5f; // Расстояние между точками патрулирования
    [SerializeField] protected float waitTime = 1f; // Время ожидания на точке
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected LayerMask groundLayer; // Слой земли

    private Vector3 pointA; // Первая точка патрулирования
    private Vector3 pointB; // Вторая точка патрулирования
    private float waitTimer; // Таймер ожидания

    protected SoundPlay soundPlay;
    protected Vector3 targetPoint; // Целевая точка, к которой движется враг

    public override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        pointA = transform.position;
        pointB = pointA + transform.right * patrolDistance;
        targetPoint = pointA; // Начинаем движение к первой точке
        soundPlay = transform.Find("Sounds").GetComponent<SoundPlay>();
    }
    private void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        Patrol();
    }

    protected void Patrol()
    {
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
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);

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
