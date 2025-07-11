using System.Collections;
using UnityEngine;

public class PlayerHealth : Player
{
    [SerializeField] private GameObject DeathEffect;
    [SerializeField] private Transitions transitions;
    [SerializeField] private bool isImmortal;

    private SpriteRenderer spriteRenderer;
    private Transform currentCheckPoint;
    private Vector2 defaultSpawnPosition;
    private BossfightManager bossfightManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        bossfightManager = FindFirstObjectByType<BossfightManager>();

        defaultSpawnPosition = transform.position;
    }

    public void Death()
    {
        if (!isImmortal)
        {
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            StartCoroutine(RespawnPlayer());

            playerController.enabled = false;
            spriteRenderer.enabled = false;
            playerCollider.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Debug.Log("Player is immortal");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            Death();
        }
    }
    public void SetCheckPoint(Transform pointTransform)
    {
        if (currentCheckPoint != null)
        {
            currentCheckPoint.GetComponent<CheckPoint>().Deactivate();
        }
        currentCheckPoint = pointTransform;
    }
    IEnumerator RespawnPlayer()
    {
        transitions.StartTransition();

        yield return new WaitForSeconds(1);

        playerController.enabled = true;
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (bossfightManager != null) { bossfightManager.RespawnBoss(); }

        if (currentCheckPoint != null)
        {
            transform.position = currentCheckPoint.position;
            currentCheckPoint.GetComponent<CheckPoint>().RestoreLevel();
        }
        else
        {
            transform.position = defaultSpawnPosition;
        }

    }
}
