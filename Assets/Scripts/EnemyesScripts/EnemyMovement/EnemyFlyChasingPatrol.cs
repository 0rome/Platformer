using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyChasingPatrol : EnemyFlyingPatrolling
{
    [Header("Chasing settings")]
    public float aggroRadius = 5f; // Радиус агра

    private Transform player; // Ссылка на игрока

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
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
        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= aggroRadius)
        {
            // Если игрок в радиусе агра, преследуем его
            agent.SetDestination(player.position);

            // Обновляем направление взгляда (вправо/влево)
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction.x > 0.1f) // Смотрим вправо
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < -0.1f) // Смотрим влево
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Включаем анимацию движения
            animator.SetFloat("speed", 1);
        }
        else
        {
            // Если игрок вне радиуса агра, возвращаемся к патрулированию
            agent.ResetPath();
            Movement();
        }
    }

    // Рисуем радиус агра в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}
