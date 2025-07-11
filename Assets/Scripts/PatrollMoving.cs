using UnityEngine;

public class PatrollMoving : MonoBehaviour
{
    [Header("Параметры патрулирования")]
    [SerializeField] private Transform objectToMove; // Объект движения
    [SerializeField] private Transform pointA; // Первая точка
    [SerializeField] private Transform pointB; // Вторая точка
    [SerializeField] private float speed = 2f; // Скорость движения

    private Vector3 targetPoint; // Текущая точка назначения

    private void Start()
    {
        // Устанавливаем начальную точку назначения
        targetPoint = pointA.position;
    }

    private void Update()
    {
        // Двигаем объект к целевой точке
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPoint, speed * Time.deltaTime);

        // Проверяем, достиг ли объект целевой точки
        if (Vector3.Distance(objectToMove.transform.position, targetPoint) < 0.1f)
        {
            // Переключаем точку назначения
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnDrawGizmos()
    {
        // Рисуем линии между точками для удобства настройки в редакторе
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
