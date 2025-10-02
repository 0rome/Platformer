using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class SpaceshipHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private Transform effectTransform;
    [SerializeField] private GameObject[] objectsToDeactivate;

    private PlayerDeathTransition playerDeathTransition;
    private SpaceshipController spaceshipController;

    private Collider2D m_Collider;
    private Vector2 startPosition;

    public static event Action SpaceshipDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;

        playerDeathTransition = GetComponentInChildren<PlayerDeathTransition>();
        spaceshipController = GetComponent<SpaceshipController>();
        m_Collider = GetComponent<Collider2D>();

    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            DestroySpaceship();
        }
    }

    public void DestroySpaceship()
    {
        SpaceshipDead?.Invoke();

        StartCoroutine(Respawn());

        if (deathEffect != null)
        {
            Instantiate(deathEffect, effectTransform.position, Quaternion.identity);
        }
        
    }

    private IEnumerator Respawn()
    {
        foreach (var obj in objectsToDeactivate) { obj.SetActive(false); }
        playerDeathTransition.StartTransition();
        m_Collider.enabled = false;
        spaceshipController.enabled = false;

        yield return new WaitForSeconds(1f);

        foreach (var obj in objectsToDeactivate) { obj.SetActive(true); }
        m_Collider.enabled = true;
        transform.position = startPosition;
        spaceshipController.enabled = true;
        transform.localScale = Vector3.one;
    }
}
