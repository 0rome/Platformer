using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class AutoShootWeapon : RangeWeapon
{
    private int currentAmmo; // Текущее количество патронов
    private bool isReloading; // Флаг перезарядки

    void Start()
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
        nextFireTime = Time.time + attackSpeed; // Устанавливаем время следующего выстрела

        soundPlay.PlaySound(0);

        currentAmmo--; // Уменьшаем количество патронов

        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Создаём пулю на месте точки выстрела
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
