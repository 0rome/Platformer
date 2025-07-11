using System.Collections;
using UnityEngine;

public class DoorOpener : Mechanism
{
    [SerializeField] private Transform pointA; // Точка A
    [SerializeField] private Transform pointB; // Точка B
    [SerializeField] private GameObject DoorObj;
    [SerializeField] private float speed = 1f; // Скорость перемещения

    private SoundPlay soundPlay;
    private bool isMoving = false; // Флаг, чтобы не начинать движение, если объект уже в пути

    // Метод для запуска движения

    private void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
    }
    public override void DoSomething()
    {
        MoveToB();
        
    }
    private void MoveToB()
    {
        if (!isMoving) // Проверяем, не в движении ли уже объект
        {
            StartCoroutine(Move(pointA.position, pointB.position));
        }
        soundPlay.PlaySound(0);
    }

    // Корутина для плавного движения
    private IEnumerator Move(Vector3 start, Vector3 end)
    {
        isMoving = true;
        float distance = Vector3.Distance(start, end);
        float elapsedTime = 0f;

        while (elapsedTime < distance / speed)
        {
            // Линейная интерполяция позиции между start и end
            DoorObj.transform.position = Vector3.Lerp(start, end, elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечную позицию, чтобы избежать неточностей
        DoorObj.transform.position = end;
        isMoving = false;
    }
    
}
