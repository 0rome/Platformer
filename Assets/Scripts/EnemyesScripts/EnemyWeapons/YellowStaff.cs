using UnityEngine;
using System.Collections;

public class YellowStaff :EnemyWeaponAim
{
    [Header("FireSettings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackSpeed = 2;


    private Animator animator;
    private float nextFireTime = 0f;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        soundPlay = GetComponent<SoundPlay>();
        animator = GetComponent<Animator>();
    }
    protected override void Fire(Vector2 targetPos)
    {
        Attack();
    }
    
    private void Attack()
    {
        if (Time.time < nextFireTime) return; // еще не настало время стрелять
        nextFireTime = Time.time + attackSpeed;

        if (projectilePrefab == null) return;

        animator.SetTrigger("Attack");

        soundPlay.PlaySound(0);

        shootEffect.Play();

        //Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(0,4), transform.position.z);
        Vector3 spawnPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 2, playerTransform.position.z);

        Instantiate(projectilePrefab, spawnPos,Quaternion.identity);

    }
}
