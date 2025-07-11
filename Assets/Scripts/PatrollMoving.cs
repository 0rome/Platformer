using UnityEngine;

public class PatrollMoving : MonoBehaviour
{
    [Header("��������� ��������������")]
    [SerializeField] private Transform objectToMove; // ������ ��������
    [SerializeField] private Transform pointA; // ������ �����
    [SerializeField] private Transform pointB; // ������ �����
    [SerializeField] private float speed = 2f; // �������� ��������

    private Vector3 targetPoint; // ������� ����� ����������

    private void Start()
    {
        // ������������� ��������� ����� ����������
        targetPoint = pointA.position;
    }

    private void Update()
    {
        // ������� ������ � ������� �����
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPoint, speed * Time.deltaTime);

        // ���������, ������ �� ������ ������� �����
        if (Vector3.Distance(objectToMove.transform.position, targetPoint) < 0.1f)
        {
            // ����������� ����� ����������
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnDrawGizmos()
    {
        // ������ ����� ����� ������� ��� �������� ��������� � ���������
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
