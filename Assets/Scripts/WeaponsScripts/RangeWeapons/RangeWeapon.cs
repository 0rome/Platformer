using UnityEngine;
using TMPro;

public abstract class RangeWeapon : Weapon
{
    [Header("Weapon Settings")]
    [SerializeField] protected GameObject bulletPrefab; // ������ ����
    [SerializeField] protected Transform firePoint; // ����� ��������
    [SerializeField] protected int maxAmmo = 30; // ������������ ���������� �������� � ��������
    [SerializeField] protected float reloadTime = 2f; // ����� �����������
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

    protected float nextFireTime; // ����� ���������� ��������
    protected SoundPlay soundPlay;

    public abstract void Fire();
}
