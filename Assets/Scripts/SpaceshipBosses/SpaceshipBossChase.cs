using UnityEngine;

public class SpaceshipBossChase : MonoBehaviour
{
    [Header("Ссылки")]
    public Transform player;    // игрок
    public Transform cam;       // камера (или объект, за которым движется камера)

    [Header("Настройки")]
    public float followSpeedX = 2f;   // скорость слежения за игроком по X
    public float offsetY = -5f;       // дистанция позади камеры по Y
    public float smoothY = 5f;        // сглаживание движения по Y (если нужно)

    void Update()
    {
        if (player == null || cam == null) return;

        // Двигаем босса по X за игроком
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, player.position.x, followSpeedX * Time.deltaTime);

        // По Y босс следует за камерой с отступом
        float targetY = cam.position.y + offsetY;
        newPos.y = Mathf.Lerp(newPos.y, targetY, smoothY * Time.deltaTime);

        transform.position = newPos;
    }
}
