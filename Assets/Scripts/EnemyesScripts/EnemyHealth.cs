using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : Enemy
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage; // ќсновной заполн€ющий бар
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject deathEffect;

    private int currentHealth;
    private Vector2 defaultPosition;
    private Coroutine fadeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        defaultPosition = transform.position;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        animator.SetTrigger("Hit");

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(SmoothHealthFade());

        if (currentHealth <= 0) Death();
    }

    private IEnumerator SmoothHealthFade()
    {
        float startValue = fillImage.fillAmount;
        float targetValue = healthSlider.value / maxHealth;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            yield return null;
        }

        fillImage.fillAmount = targetValue;
    }
    public void Respawn()
    {
        gameObject.SetActive(true);
        
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        transform.position = defaultPosition;
    }
    private void Death()
    {
        Instantiate(deathEffect, transform.position,Quaternion.identity);
        gameObject.SetActive(false);
    }
}
