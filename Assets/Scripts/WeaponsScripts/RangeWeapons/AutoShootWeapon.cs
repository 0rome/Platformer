using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class AutoShootWeapon : RangeWeapon
{
    [SerializeField] private float bulletSpeed = 10f;
    [Range(0f, 100f)]
    [SerializeField] private float spread = 5f; // кучность, градусы

   private int currentAmmo; // Текущее количество патронов
    private bool isReloading; // Флаг перезарядки

    protected void Start()
    {
        currentAmmo = maxAmmo; // Устанавливаем полное количество патронов

        bulletsCountText.text = currentAmmo.ToString();

        weaponInformationUI = GetComponent<WeaponInformationUI>();

        soundPlay = transform.Find("Sounds").GetComponent<SoundPlay>();
    }
    
    protected void Update()
    {
        if (weaponIsActive)
        {
            if (isReloading && Input.GetMouseButton(0)) 
            {
                soundPlay.PlaySound(3);
                return;
            } 

            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                if (currentAmmo > 0)
                {
                    Fire();
                }
            }
            if(currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }
        }
        
    }

    public override void Fire()
    {
        nextFireTime = Time.time + attackSpeed;

        soundPlay.PlaySound(0);
        currentAmmo--;

        // создаём пулю без поворота (мы сами задаём направление)
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // базовое направление (куда смотрит firePoint)
        Vector2 dir = firePoint.right; // если ось X – дуло
                                       // Vector2 dir = firePoint.up; // если ось Y – дуло

        // случайное смещение в пределах spread
        float randomAngle = UnityEngine.Random.Range(-spread, spread);
        dir = Quaternion.Euler(0, 0, randomAngle) * dir;

        // применяем скорость
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir.normalized * bulletSpeed;

        bullet.GetComponent<Bullet>().SetDamage(damage);


        bulletsCountText.text = currentAmmo.ToString();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading..."); // Для отладки (можно заменить на визуальный эффект)
        yield return new WaitForSeconds(reloadTime); // Ждём указанное время

        currentAmmo = maxAmmo; // Заполняем патроны до максимума
        isReloading = false;

        soundPlay.PlaySound(1);

        bulletsCountText.text = currentAmmo.ToString();

        Debug.Log("Reload Complete"); // Для отладки
    }

    public override void TakeGun()
    {
        base.TakeGun();
        soundPlay.PlaySound(1);
        weaponInformationUI.Disable();
    }
    public override void ThrowGun()
    {
        base.ThrowGun();
        soundPlay.PlaySound(2);
    }
}
