using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // Ссылка на игрока
    [SerializeField] private float followRadius = 3f; // Радиус следования за курсором
    [SerializeField] private float followSpeed = 5f; // Скорость следования камеры

    [Header("Player Flip Settings")]
    [SerializeField] private SpriteRenderer playerSprite; // Спрайт игрока (для переворота)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (playerSprite == null)
        {
            playerSprite = player.GetComponent<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Получаем позицию курсора в мировых координатах
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // Устанавливаем Z для 2D сцены

        // Вычисляем направление от игрока к курсору
        Vector3 targetPosition = player.position + Vector3.ClampMagnitude(mouseWorldPosition - player.position, followRadius);

        // Плавно перемещаем камеру
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), followSpeed * Time.deltaTime);

        // Определяем направление и переворачиваем игрока
        FlipPlayer(mouseWorldPosition);
    }

    private void FlipPlayer(Vector3 mousePosition)
    {
        // Если курсор находится слева от игрока, поворачиваем его влево, иначе вправо
        if (mousePosition.x < player.position.x)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }
    }
}
