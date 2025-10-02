using UnityEngine;

public class SpaceshipBossTrigger : MonoBehaviour
{
    [Header("Commands: Gun_1, Gun_2, Gun_both, Laser_1, Laser_2, Laser_both")]
    [SerializeField] private string AttackName;

    private SpaceshipBossController bossController;
    void Start()
    {
        bossController = FindFirstObjectByType<SpaceshipBossController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (AttackName)
            {
                case "Gun_1":
                    bossController.AttackGun_1();
                    break;
                case "Gun_2":
                    bossController.AttackGun_2();
                    break;
                case "Gun_both":
                    bossController.AttackGunsBoth();
                    break;
                case "Laser_1":
                    bossController.AttackLaser_1();
                    break;
                case "Laser_2":
                    bossController.AttackLaser_2();
                    break;
                case "Laser_both":
                    bossController.AttackLasersBoth();
                    break;
                default:
                    Debug.Log("Wrong command");
                    break;
            }
        }
    }
}
