using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyChasingPatrol : MonoBehaviour
{
    [Header("Settings")]
    public float aggroRadius = 8f;       // радиус агра
    public float stoppingDistance = 1f;  // дистанция остановки перед игроком

    private Transform player;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ⚠️ Обязательно для 2D: фиксируем движение в XY-плоскости
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= aggroRadius)
        {
            // Летим за игроком
            agent.isStopped = false;
            agent.stoppingDistance = stoppingDistance;
            agent.SetDestination(player.position);
        }
        else
        {
            // Не агримся — стоим
            agent.isStopped = true;
        }

        // Разворот по направлению движения (влево/вправо)
        if (agent.velocity.x > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (agent.velocity.x < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}

