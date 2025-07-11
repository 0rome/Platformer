using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerHealth playerHealth;
    protected PlayerController playerController;
    protected Rigidbody2D rb;
    protected PolygonCollider2D playerCollider;
    protected CameraFollow2D camFollow;
    protected GameObject FlashLight;
    protected Inventory inventory;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PolygonCollider2D>();
        //camFollow = Camera.main.GetComponent<CameraFollow2D>();
        FlashLight = transform.Find("Flashlight").gameObject;
        inventory = GetComponent<Inventory>();
    }
    public void DeactivatePlayer()
    {
        playerHealth.enabled = false;
        playerController.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.enabled = false;
        //camFollow.enabled = false;
        FlashLight.SetActive(false);
        inventory.enabled = false;
    }
    public void ActivatePlayer()
    {
        playerHealth.enabled = true;
        playerController.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        playerCollider.enabled = true;
        //camFollow.enabled = true;
        FlashLight.SetActive(true);
        inventory.enabled = true;
    }
}
