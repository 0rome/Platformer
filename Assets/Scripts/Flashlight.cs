using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private Transform player; // Ссылка на объект игрока
    [SerializeField] private float rotationOffsetZ = 0f; // Оффсет вращения в градусах по оси z

    private Camera mainCamera; // Камера для отслеживания курсора
    private SoundPlay soundPlay;
    private Light2D flashlight;

    private void Awake()
    {
        soundPlay = GetComponent<SoundPlay>();
        flashlight = GetComponent<Light2D>();

        // Если камера не назначена, автоматически выбираем основную
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    private void Update()
    {
        FLashlightButton();
        FollowPlayer();
        RotateTowardsCursor();
    }

    private void FLashlightButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.enabled == true)
            {
                flashlight.enabled = false;
            }
            else
            {
                flashlight.enabled = true;
            }
            
            soundPlay.PlaySound(0);
        }
    }
    private void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.position;
        }
    }

    private void RotateTowardsCursor()
    {
        // Получаем мировую позицию курсора
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Вычисляем направление к курсору
        Vector3 direction = worldMousePosition - transform.position;
        direction.z = 0f; // Игнорируем компонент Z, чтобы вращение было в 2D

        // Рассчитываем угол и применяем оффсет
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += rotationOffsetZ;

        // Устанавливаем вращение с учётом оффсета
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
