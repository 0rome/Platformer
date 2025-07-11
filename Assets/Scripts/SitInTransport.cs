using UnityEngine;

public class SitInTransport : MonoBehaviour
{
    private GameObject currentTransport;

    private CapsuleCollider2D Collider;
    private PlayerController controller;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTransport != null)
        {
            Out();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "MovableTransport" || collision.collider.tag == "StaticTransport")
        {
            if (currentTransport != null)
                return;

            currentTransport = collision.collider.gameObject;

            Sit();
        }
    }
    private void Sit()
    {
        Collider.enabled = false;
        controller.enabled = false;
        if (currentTransport.tag == "MovableTransport")
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (currentTransport.tag == "StaticTransport")
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
        currentTransport.GetComponent<Transport>().enabled = true;

        transform.position = currentTransport.transform.position;
        transform.rotation = currentTransport.transform.rotation;
        transform.SetParent(currentTransport.transform);
    }
    private void Out()
    {
        Collider.enabled = true;
        controller.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        currentTransport.GetComponent<Transport>().enabled = false;

        transform.position = new Vector2(currentTransport.transform.position.x - 2, currentTransport.transform.position.y);
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);
        currentTransport = null;
    }
}
