using UnityEngine;
using DG.Tweening;
using System.Collections;

public class LaserBomb : Traps
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float projectileSpeed = 25f;
    
    private bool isActive = true;
    private SoundPlay soundPlay;
    private SpriteRenderer spriteRenderer;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        soundPlay = GetComponent<SoundPlay>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Activate());
        }
    }
    
    private IEnumerator Activate()
    {
        if (isActive && projectilePrefab != null)
        {
            transform.parent.DOMoveY(transform.position.y + 1.5f, 0.25f);
            transform.DOMoveY(transform.position.y + 1.5f, 0.25f);
            transform.DOScaleX(0, 0.25f);
            soundPlay.PlaySound(0);

            yield return new WaitForSeconds(0.25f);

            soundPlay.PlaySound(1);
            Shoot();

            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No projectile!");
        }
    }
    private void Shoot()
    {
        // Создаём снаряд из префаба
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);

        // Находим 2 Rigidbody2D у дочерних объектов
        Rigidbody2D rb0 = projectile.transform.GetChild(0).GetComponent<Rigidbody2D>();
        Rigidbody2D rb1 = projectile.transform.GetChild(1).GetComponent<Rigidbody2D>();

        if (rb0 != null && rb1 != null)
        {
            // Вычисляем направление
            Vector2 dir = transform.right.normalized;

            rb0.linearVelocity = dir * projectileSpeed;
            rb1.linearVelocity = -dir * projectileSpeed;
        }
        else
        {
            Debug.LogError("Не найдены Rigidbody2D у дочерних объектов префаба!");
        }
    }
}
