using UnityEngine;

public class WeaponFollowCursor : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 originalScale;

    void Start()
    {
        mainCamera = Camera.main;
        originalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.localScale = new Vector3(originalScale.x, direction.x < 0 ? -originalScale.y : originalScale.y, originalScale.z);
    }
}
