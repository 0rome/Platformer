using UnityEngine;

public class ArrowTrap : Traps
{
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform shotPoint;

    private bool isActive = true;
    private SoundPlay soundPlay;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void Start()
    {
        base.Start();

        soundPlay = GetComponent<SoundPlay>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isActive)
        {
            animator.SetTrigger("Activate");
        }
    }

    private void ActivateTrap()
    {
        GameObject arrow = Instantiate(Projectile, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shotPoint.up * -30f; // скорость 10, можно вынести в поле
        soundPlay.PlaySound(0);
        isActive = false;
        Destroy(arrow,5f);
    }
}
