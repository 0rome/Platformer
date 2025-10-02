using UnityEngine;

public class InfiniteScroll2D : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float speed = 2f;   // скорость движения
    [SerializeField] private Transform sprite1;  // первый спрайт
    [SerializeField] private Transform sprite2;  // второй спрайт

    private float spriteHeight;

    void Start()
    {
        if (sprite1 == null || sprite2 == null)
        {
            Debug.LogError("Назначьте оба спрайта!");
            enabled = false;
            return;
        }

        // точная высота с учётом масштаба
        spriteHeight = sprite1.GetComponent<SpriteRenderer>().sprite.bounds.size.y * sprite1.localScale.y;

        // выставляем sprite2 ровно над sprite1
        sprite2.position = sprite1.position + Vector3.up * spriteHeight;
    }

    void Update()
    {
        // Двигаем оба спрайта вниз
        sprite1.position += Vector3.down * speed * Time.deltaTime;
        sprite2.position += Vector3.down * speed * Time.deltaTime;

        // Проверяем, ушёл ли sprite1 вниз
        if (sprite1.position.y <= -spriteHeight)
        {
            sprite1.position = sprite2.position + Vector3.up * spriteHeight;
        }

        // Проверяем, ушёл ли sprite2 вниз
        if (sprite2.position.y <= -spriteHeight)
        {
            sprite2.position = sprite1.position + Vector3.up * spriteHeight;
        }
    }
}
