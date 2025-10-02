using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    protected Animator animator;
    protected SoundPlay soundPlay;
    protected Enemy currentEnemy;

    private void Awake()
    {
        currentEnemy = GetComponent<Enemy>();
    }
    protected virtual void Start()
    {
        soundPlay = GetComponentInChildren<SoundPlay>();
        animator = GetComponent<Animator>();
        
    }
    public abstract void Ability();
}
