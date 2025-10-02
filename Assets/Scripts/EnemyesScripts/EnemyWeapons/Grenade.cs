using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 2f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private Collider2D explosionTriggerCollider;

    private SpriteRenderer explosionRenderer;

    private void Start()
    {
        StartCoroutine(Explode());
        explosionRenderer = GetComponent<SpriteRenderer>();
    }
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeToDestroy);

        CameraShake.instance.Shake(0.2f,0.5f);
        explosionRenderer.enabled = false;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        explosionTriggerCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
