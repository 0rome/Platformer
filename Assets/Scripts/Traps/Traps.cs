using UnityEngine;

public class Traps : MonoBehaviour
{
    private Vector3 defaultPosition;

    public void Start()
    {
        defaultPosition = transform.position; // ��������� �������
    }

    public void RestoreTrap()
    {
        transform.position = defaultPosition; // ��������������� �������
        gameObject.SetActive(true);
    }
}
