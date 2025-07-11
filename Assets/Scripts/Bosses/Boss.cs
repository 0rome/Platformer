using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    protected Animator animator;
    protected SoundPlay soundPlay;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        soundPlay = GetComponentInChildren<SoundPlay>();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
