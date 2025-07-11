using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private EnemyHealth[] Enemyes;

    [SerializeField] private Traps[] Traps;

    [SerializeField] private Boss[] Bosses;

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
    void Update()
    {
        if (Player.activeSelf == false)
        {
            Player.transform.position = transform.position;
            Player.SetActive(true);
        }
    }
    public void RestoreLevel()
    {
        if (Enemyes.Length != 0)
        {
            for (int i = 0; i < Enemyes.Length; i++)
            {
                Enemyes[i].Respawn();
            }
        }
        if (Traps.Length != 0)
        {
            for (int i = 0; i < Traps.Length; i++)
            {
                Traps[i].RestoreTrap();
            }
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
