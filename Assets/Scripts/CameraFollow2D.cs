using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform player; // ������ �� ������
    [SerializeField] private float followRadius = 3f; // ������ ���������� �� ��������
    [SerializeField] private float followSpeed = 5f; // �������� ���������� ������

    [Header("Player Flip Settings")]
    [SerializeField] private SpriteRenderer playerSprite; // ������ ������ (��� ����������)

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

        // �������� ������� ������� � ������� �����������
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // ������������� Z ��� 2D �����

        // ��������� ����������� �� ������ � �������
        Vector3 targetPosition = player.position + Vector3.ClampMagnitude(mouseWorldPosition - player.position, followRadius);

        // ������ ���������� ������
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), followSpeed * Time.deltaTime);

        // ���������� ����������� � �������������� ������
        FlipPlayer(mouseWorldPosition);
    }

    private void FlipPlayer(Vector3 mousePosition)
    {
        // ���� ������ ��������� ����� �� ������, ������������ ��� �����, ����� ������
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
