using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Player
{
    public bool isImmortal;

    [SerializeField] private GameObject DeathEffect;

    private SpriteRenderer spriteRenderer;
    private Transform currentCheckPoint;
    private Vector2 defaultSpawnPosition;
    private BossfightManager bossfightManager;
    private PlayerDeathTransition playerDeathTransition;

    public static event Action OnDead;

    private void Start()
    {
        playerDeathTransition = GetComponentInChildren<PlayerDeathTransition>();

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

            OnDead?.Invoke();
        }
        else
        {
            Debug.Log("Player is immortal");
        }
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
        if (currentCheckPoint != null && currentCheckPoint.GetComponent<CheckPoint>() != null)
        {
            currentCheckPoint.GetComponent<CheckPoint>().Deactivate();
        }
        currentCheckPoint = pointTransform;
    }
    IEnumerator RespawnPlayer()
    {

        playerDeathTransition.StartTransition();

        yield return new WaitForSeconds(1);

        
        playerController.enabled = true;
        spriteRenderer.enabled = true;
        playerCollider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (bossfightManager != null) { bossfightManager.RespawnBoss(); }

        if (currentCheckPoint != null && currentCheckPoint.GetComponent<CheckPoint>() != null)
        {
            currentCheckPoint.GetComponent<CheckPoint>().RestoreLevel();
        }
        else
        {
            transform.position = defaultSpawnPosition;
        }

    }
}
