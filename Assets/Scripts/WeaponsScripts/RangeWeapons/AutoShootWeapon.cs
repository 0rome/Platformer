using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class AutoShootWeapon : RangeWeapon
{
    private int currentAmmo; // ������� ���������� ��������
    private bool isReloading; // ���� �����������

    void Start()
    {
        currentAmmo = maxAmmo; // ������������� ������ ���������� ��������

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
        nextFireTime = Time.time + attackSpeed; // ������������� ����� ���������� ��������

        soundPlay.PlaySound(0);

        currentAmmo--; // ��������� ���������� ��������

        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // ������ ���� �� ����� ����� ��������
        bullet.GetComponent<Bullet>().SetDamage(damage);

        bulletsCountText.text = currentAmmo.ToString();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading..."); // ��� ������� (����� �������� �� ���������� ������)
        yield return new WaitForSeconds(reloadTime); // ��� ��������� �����

        currentAmmo = maxAmmo; // ��������� ������� �� ���������
        isReloading = false;

        soundPlay.PlaySound(1);

        bulletsCountText.text = currentAmmo.ToString();

        Debug.Log("Reload Complete"); // ��� �������
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
