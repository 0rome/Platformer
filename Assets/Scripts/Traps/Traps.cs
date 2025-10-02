using UnityEngine;

public class Traps : MonoBehaviour
{
    private Vector3 defaultPosition;

    public virtual void Start()
    {
        defaultPosition = transform.position; // Сохраняем позицию
    }

    public void RestoreTrap()
    {
        transform.position = defaultPosition; // Восстанавливаем позицию
        gameObject.SetActive(true);
    }
}
