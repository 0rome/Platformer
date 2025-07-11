using UnityEngine;

public class TilemapParalax : MonoBehaviour
{
    public float parallaxEffect = 0.5f;
    public float maxOffset = 0.03f; // Ограничение смещения
    private Camera mainCamera;
    private Vector3 lastCameraPosition;

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraPosition = mainCamera.transform.position;
    }

    void LateUpdate()
    {
        Vector3 currentCameraPosition = mainCamera.transform.position;
        Vector3 deltaMovement = currentCameraPosition - lastCameraPosition;

        // Ограничиваем максимальное смещение
        deltaMovement.x = Mathf.Clamp(deltaMovement.x * parallaxEffect, -maxOffset, maxOffset);
        deltaMovement.y = Mathf.Clamp(deltaMovement.y * parallaxEffect, -maxOffset, maxOffset);

        transform.position += new Vector3(deltaMovement.x, deltaMovement.y, 0);
        lastCameraPosition = currentCameraPosition;
    }
}
