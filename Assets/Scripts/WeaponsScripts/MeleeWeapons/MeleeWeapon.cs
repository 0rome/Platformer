using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected Animator animator;
    protected SoundPlay soundPlay;

    public abstract void Attack();

    private void Start()
    {
        var Blade = transform.GetChild(0).GetComponent<Blade>();
        Blade.SetDamage(damage);

        weaponInformationUI = GetComponent<WeaponInformationUI>();
        soundPlay = transform.Find("Sounds").GetComponent<SoundPlay>();
        animator = GetComponent<Animator>();
    }
}
