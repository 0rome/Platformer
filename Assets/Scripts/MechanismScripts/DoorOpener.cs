using System.Collections;
using UnityEngine;

public class DoorOpener : Mechanism
{
    [SerializeField] private Transform pointA; // ����� A
    [SerializeField] private Transform pointB; // ����� B
    [SerializeField] private GameObject DoorObj;
    [SerializeField] private float speed = 1f; // �������� �����������

    private SoundPlay soundPlay;
    private bool isMoving = false; // ����, ����� �� �������� ��������, ���� ������ ��� � ����

    // ����� ��� ������� ��������

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
        if (!isMoving) // ���������, �� � �������� �� ��� ������
        {
            StartCoroutine(Move(pointA.position, pointB.position));
        }
        soundPlay.PlaySound(0);
    }

    // �������� ��� �������� ��������
    private IEnumerator Move(Vector3 start, Vector3 end)
    {
        isMoving = true;
        float distance = Vector3.Distance(start, end);
        float elapsedTime = 0f;

        while (elapsedTime < distance / speed)
        {
            // �������� ������������ ������� ����� start � end
            DoorObj.transform.position = Vector3.Lerp(start, end, elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������������� �������� �������, ����� �������� �����������
        DoorObj.transform.position = end;
        isMoving = false;
    }
    
}
