using UnityEngine;

public class SitInTransport : MonoBehaviour
{
    private GameObject currentTransport;

    private Collider2D Collider;
    private Inventory inventory;
    private Player player;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider = GetComponent<Collider2D>();
        player = GetComponent<Player>();
        inventory = player.gameObject.GetComponent<Inventory>();
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
        inventory.enabled = false;
        Collider.enabled = false;
        player.enabled = false;
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
        inventory.enabled = true;
        Collider.enabled = true;
        player.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        currentTransport.GetComponent<Transport>().enabled = false;

        transform.position = new Vector2(currentTransport.transform.position.x - 2, currentTransport.transform.position.y);
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);
        currentTransport = null;
    }
}
