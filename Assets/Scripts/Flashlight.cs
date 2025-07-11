using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private Transform player; // ������ �� ������ ������
    [SerializeField] private float rotationOffsetZ = 0f; // ������ �������� � �������� �� ��� z

    private Camera mainCamera; // ������ ��� ������������ �������
    private SoundPlay soundPlay;
    private Light2D flashlight;

    private void Awake()
    {
        soundPlay = GetComponent<SoundPlay>();
        flashlight = GetComponent<Light2D>();

        // ���� ������ �� ���������, ������������� �������� ��������
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
        // �������� ������� ������� �������
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // ��������� ����������� � �������
        Vector3 direction = worldMousePosition - transform.position;
        direction.z = 0f; // ���������� ��������� Z, ����� �������� ���� � 2D

        // ������������ ���� � ��������� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += rotationOffsetZ;

        // ������������� �������� � ������ �������
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
