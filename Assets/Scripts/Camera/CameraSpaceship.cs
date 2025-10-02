using UnityEngine;

public class CameraSpaceship : MonoBehaviour
{
    [Header("Цель и настройки")]
    public Transform target;          // объект, за которым следить
    public float smoothSpeed = 0.125f; // плавность движения
    public Vector3 offset;            // смещение камеры от объекта

    [Header("Ограничения камеры")]

    public float minX, maxX;
    public float minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;

        // Желаемая позиция с учётом смещения
        Vector3 desiredPosition = target.position + offset;

        // Ограничиваем по X и Y
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z -10);

        // Плавное движение камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
