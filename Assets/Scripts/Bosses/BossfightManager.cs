using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class BossfightManager : MonoBehaviour
{
    public Boss bossPrefab;
    public Transform bossSpawnTransform;
    public AudioSource music;
    public AudioSource bossDeathSound;

    public GameObject[] doors;

    private BoxCollider2D mBoxCollider;
    private PlayerHealth player;
    private Boss currentBoss;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBoss = Instantiate(bossPrefab,bossSpawnTransform);
        currentBoss.gameObject.SetActive(false);

        player = FindFirstObjectByType<PlayerHealth>();
        mBoxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentBoss.gameObject.activeSelf)
        {
            music.Stop(); 
        }
    }
    public void BossIsDead()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetTrigger("End");
        }
        animator.SetTrigger("isDead");

        bossDeathSound.Play();
        music.Stop();
        PostProcessingEffects.StartColorAdjustmentAnimation(0,100,1);
        PostProcessingEffects.StartLensDistortionAnimation(0,1,1);
    }
    public void RespawnBoss()
    {
        music.Stop();
        mBoxCollider.enabled = true;

        currentBoss.Destroy();
        currentBoss = Instantiate(bossPrefab, bossSpawnTransform);
        currentBoss.gameObject.SetActive(false);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetTrigger("End");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            music.Play();
            currentBoss.gameObject.SetActive(true);
            mBoxCollider.enabled = false;


            for (int i = 0; i < doors.Length; i++)
            {
                //doors[i].gameObject.SetActive(true);
                doors[i].GetComponent<Animator>().SetTrigger("Start");
            }
        }
    }
}
