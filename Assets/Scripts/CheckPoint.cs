using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private EnemyHealth[] Enemyes;

    [SerializeField] private Traps[] traps;

    private SoundPlay soundPlay;
    private Animator animator;
    private GameObject Player;

    void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    
    public void RestoreLevel()
    {
        
        foreach (var enemy in Enemyes)
        {
            enemy.Respawn();
        }
        foreach (var trap in traps)
        {
            trap.RestoreTrap();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (animator.GetBool("isActive") == false)
            {
                Activate();
                collision.GetComponent<PlayerHealth>().SetCheckPoint(transform);
            }
        }
    }
    public void Activate()
    {
        soundPlay.PlaySound(0);
        animator.SetBool("isActive", true);
    }
    public void Deactivate()
    {
        animator.SetBool("isActive", false);
    }
}
