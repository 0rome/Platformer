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
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<PolygonCollider2D>();
        camFollow = Camera.main.GetComponent<CameraFollow2D>();
        FlashLight = transform.Find("Flashlight").gameObject;
        inventory = GetComponent<Inventory>();
    }
    public void DeactivatePlayer()
    {
        
        playerHealth.isImmortal = true;
        playerController.enabled = false;
        animator.SetFloat("Speed",0);
        //rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        //playerCollider.enabled = false;
        camFollow.enabled = false;
        FlashLight.SetActive(false);
        inventory.enabled = false;
    }
    public void ActivatePlayer()
    {
       
        playerHealth.isImmortal = false;
        playerController.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        playerCollider.enabled = true;
        camFollow.enabled = true;
        FlashLight.SetActive(true);
        inventory.enabled = true;
    }
}
