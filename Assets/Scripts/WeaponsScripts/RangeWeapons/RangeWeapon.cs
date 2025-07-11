using UnityEngine;
using TMPro;

public abstract class RangeWeapon : Weapon
{
    [Header("Weapon Settings")]
    [SerializeField] protected GameObject bulletPrefab; // Префаб пули
    [SerializeField] protected Transform firePoint; // Точка выстрела
    [SerializeField] protected int maxAmmo = 30; // Максимальное количество патронов в магазине
    [SerializeField] protected float reloadTime = 2f; // Время перезарядки
    [SerializeField] protected TextMeshProUGUI bulletsCountText;

    public int Ammo
    {
        get { return maxAmmo; }
    }

    public override void TakeGun()
    {
        base.TakeGun();
        bulletsCountText.gameObject.SetActive(true);
    }
    public override void ThrowGun()
    {
        base.ThrowGun();
        bulletsCountText.gameObject.SetActive(false);
    }
    public override void DeActiveGun()
    {
        base.DeActiveGun();
        bulletsCountText.gameObject.SetActive(false);
    }

    protected float nextFireTime; // Время следующего выстрела
    protected SoundPlay soundPlay;

    public abstract void Fire();
}
