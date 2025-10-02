using UnityEngine;

public class SpaceshipBossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;

    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentHealth += maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
    }
}
