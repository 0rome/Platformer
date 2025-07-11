using System.Collections;
using UnityEngine;

public class SingleAttackMelee : MeleeWeapon
{
    [Header("Weapon settings")]
    private bool timeToAttack = true;

    // Update is called once per frame
    void Update()
    {
        if (weaponIsActive)
        {
            if (timeToAttack)
            {
                Attack();
            }
            
        }
        
    }
    public override void TakeGun()
    {
        base.TakeGun();
        animator.enabled = true;
        weaponInformationUI.Disable();
        soundPlay.PlaySound(3);
    }
    public override void ThrowGun()
    {
        base.ThrowGun();
        animator.enabled = false;
        soundPlay.PlaySound(4);
    }
    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            soundPlay.PlaySound(0);

            animator.SetTrigger("Attack");

            StartCoroutine(AttackDelay());
        }
    }
    private IEnumerator AttackDelay ()
    {
        timeToAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        //transform.localPosition = Vector3.zero;
        timeToAttack = true;
    }
}
