using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyingPatrolling : EnemyMovement
{
    [Header("Patrol Settings")]
    [SerializeField] private float patrolDistance = 5f; // Расстояние между точками патрулирования
    [SerializeField] private float waitTime = 1f; // Время ожидания на точке

    protected NavMeshAgent agent;

    private Vector3 pointA; // Первая точка патрулирования
    private Vector3 pointB; // Вторая точка патрулирования
    private float waitTimer; // Таймер ожидания

    protected Vector3 targetPoint; // Целевая точка, к которой движется враг

    protected void Start()
    {
        animator = GetComponent<Animator>();

        // Настройки NavMeshAgent для 2D
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false; // Отключает ориентацию вверх
        agent.updateRotation = false; // Отключает автоматическое вращение

        // Инициализация точек патрулирования на основе текущей позиции врага
        pointA = transform.position;
        pointB = pointA + transform.right * patrolDistance;
        targetPoint = pointA; // Начинаем движение к первой точке
    }

    private void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        Patrol();
    }

    private void Patrol()
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
            agent.SetDestination(targetPoint);

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
