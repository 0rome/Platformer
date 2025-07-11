using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Boss
{
    public Slider healthSlider;

    [SerializeField] private GameObject DeathEffect;


   [SerializeField] private float maxBossHealth = 2000;

    private float currentBossHealth;

    private BossfightManager bossfightManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        base.Start();

        currentBossHealth = maxBossHealth;
        bossfightManager = FindFirstObjectByType<BossfightManager>();

        healthSlider.maxValue = currentBossHealth;
        healthSlider.value = currentBossHealth;
    }

    public float GetCurrentHealth()
    {
        return currentBossHealth;
    }
    public float GetMaxHealth()
    {
        return maxBossHealth;
    }

    // Update is called once per frame

    public void GetDamage(float damage)
    {
        currentBossHealth -= damage;
        healthSlider.value = currentBossHealth;
        animator.SetTrigger("Hit");

        if (currentBossHealth < maxBossHealth / 2)
        {
            //fase 2
        }
        if (currentBossHealth <= 0)
        {
            PostProcessingEffects.StartLensDistortionAnimation(0, -1f, 5);

            gameObject.GetComponent<BossController>().enabled = false;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;

            animator.SetTrigger("Death");
            Invoke(nameof(DeactiveBoss), 3f); // Отключит через 3 секунды
        }
    }
    
    public void GetHeal(float health)
    {
        currentBossHealth += health;
        healthSlider.value = currentBossHealth;

        animator.SetTrigger("health");
    }
    public void RestoreHealth()
    {
        currentBossHealth = maxBossHealth;
    }
    private void Death()
    {
        Instantiate(DeathEffect,transform.position,Quaternion.identity);
    }
    private void DeactiveBoss()
    {
        bossfightManager.BossIsDead();
        animator.enabled = false; // Отключаем аниматор
        gameObject.SetActive(false);
    }
}
