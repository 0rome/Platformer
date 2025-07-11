using UnityEngine;
using UnityEngine.AI;

public class EnemyFlyingPatrolling : EnemyMovement
{
    [Header("Patrol Settings")]
    [SerializeField] private float patrolDistance = 5f; // ���������� ����� ������� ��������������
    [SerializeField] private float waitTime = 1f; // ����� �������� �� �����

    protected NavMeshAgent agent;

    private Vector3 pointA; // ������ ����� ��������������
    private Vector3 pointB; // ������ ����� ��������������
    private float waitTimer; // ������ ��������

    protected Vector3 targetPoint; // ������� �����, � ������� �������� ����

    protected void Start()
    {
        animator = GetComponent<Animator>();

        // ��������� NavMeshAgent ��� 2D
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false; // ��������� ���������� �����
        agent.updateRotation = false; // ��������� �������������� ��������

        // ������������� ����� �������������� �� ������ ������� ������� �����
        pointA = transform.position;
        pointB = pointA + transform.right * patrolDistance;
        targetPoint = pointA; // �������� �������� � ������ �����
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
                targetPoint = (targetPoint == pointA) ? pointB : pointA; // ������ ���� �� ��������������� �����
            }
        }
        else
        {
            // ������� ����� � ������� �����
            agent.SetDestination(targetPoint);

            // ������������ ����������� ��������
            Vector3 direction = (targetPoint - transform.position).normalized;

            // ���� ���� ��������, ��������� ����������� (������ ��� �����)
            if (direction.x > 0.1f) // �������� ������
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < -0.1f) // �������� �����
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // ���������, ������ �� ���� ������� �����
            if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
            {
                waitTimer = waitTime; // ������������� ������ ��������
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            // ���������� ����� �������������� � ������ ��������������
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
