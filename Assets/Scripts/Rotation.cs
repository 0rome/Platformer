using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 50f, 0f); // Скорость вращения по каждой оси (градусы в секунду)

    void Update()
    {
        // Вращаем объект
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
