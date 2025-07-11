using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SkullController : BossController
{
    [Header("Settings")]
    [SerializeField] private GameObject[] Projectiles;
    [SerializeField] private Transform fireballTransform;
    [SerializeField] private float speed = 5f;

    private BossHealth bossHealth;
    private Transform player;
    private Vector2 playerPos;
    private bool canMove = true;
    private bool dash;

    private bool fase_2;



    private void Awake()
    {
        bossHealth = GetComponent<BossHealth>();
        player = FindFirstObjectByType<Player>().transform;
    }

    protected override void PerformAttack()
    {
        int randomAttack = Random.Range(0, 5);
        int randomAttackFase_2 = Random.Range(6, 11);

        if (!fase_2)
        {
            switch (randomAttack)
            {
                case 0:
                    soundPlay.PlaySound(0);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.7f, 3);
                    animator.SetTrigger("Dash");
                    break;
                case 1:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_1");
                    break;
                case 2:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_2");
                    break;
                case 3:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_3");
                    break;
                case 4:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_4");
                    break;
            }
        }
        else
        {
            switch (randomAttackFase_2)
            {
                case 6:
                    soundPlay.PlaySound(0);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.7f, 3);
                    animator.SetTrigger("Dash_f2");
                    break;
                case 7:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_1_f2");
                    break;
                case 8:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_2_f2");
                    break;
                case 9:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_3_f2");
                    break;
                case 10:
                    soundPlay.PlaySound(1);
                    PostProcessingEffects.StartLensDistortionAnimation(0, -0.5f, 3);
                    animator.SetTrigger("Skill_4_f2");
                    break;
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!fase_2 && bossHealth.GetCurrentHealth() <= bossHealth.GetMaxHealth() / 2)
        {
            Fase_2(); // Вызовем только один раз
        }

        ChasePlayer();

        if (dash)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * 3 * Time.deltaTime);
            if (Vector2.Distance(transform.position, playerPos) < 0.1f)
            {
                //canMove = true;
                dash = false;
            }
        }

        
    }

    private void Fase_2()
    {
        animator.SetTrigger("Fase_2");
        fase_2 = true;
        soundPlay.PlaySound(1);
        CameraShake.instance.Shake(0.3f,0.3f);
    }


    private void ChasePlayer()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position + new Vector3(0, 8, 0), speed * Time.deltaTime);
        }
    }
    private void DisableMovement()
    {
        canMove = false;
    }
    private void EnableMovement()
    {
        canMove = true;
    }
    private void DashAttack()
    {
        canMove = false;

        playerPos = player.position;
        dash = true;
    }

    private void Skill_1()
    {
        float offsetX = Random.Range(0, 2) == 0 ? -10 : 10;

        Instantiate(Projectiles[0], transform.position + new Vector3(offsetX, 0, 0), Quaternion.identity);
    }

    private void Skill_2()
    {
        Instantiate(Projectiles[1], fireballTransform.position, Quaternion.identity);
    }
    private void Skill_3()
    {
        Instantiate(Projectiles[2], fireballTransform.position, Quaternion.identity);
    }

    private void Skill_4()
    {
        canMove = false;
        Instantiate(Projectiles[3], fireballTransform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
    /// <summary>
    /// //////////////////////////////////////////
    /// </summary>
    private void Skill_1_f2()
    {
        Instantiate(Projectiles[4], transform.position,Quaternion.identity);
        CameraShake.instance.Shake(0.2f, 0.2f);
    }
    private void Skill_2_f2()
    {
        Instantiate(Projectiles[5], fireballTransform.position, Quaternion.identity);
        CameraShake.instance.Shake(0.2f, 0.2f);
    }
    private void Skill_3_f2()
    {
        Instantiate(Projectiles[6], fireballTransform.position, Quaternion.identity);
        CameraShake.instance.Shake(0.2f, 0.2f);
    }
    private void Skill_4_f2()
    {
        canMove = false;
        Instantiate(Projectiles[7], fireballTransform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        CameraShake.instance.Shake(0.2f, 0.2f);
    }
}