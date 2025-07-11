using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : Player
{
    public Weapon currentWeapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentWeapon != null)
        {
            ThrowWeapon();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Weapon>() && Input.GetKey(KeyCode.E))
        {
            if (currentWeapon == null)
            {
                currentWeapon = collision.GetComponent<Weapon>();
                TakeWeapon();
            }
            else
            {
                
            }
        }
        
    }
    private void TakeWeapon()
    {
        currentWeapon.transform.SetParent(transform, gameObject);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.TakeGun();
    }
    private void ThrowWeapon()
    {
        currentWeapon.transform.SetParent(null);
        currentWeapon.ThrowGun();
        currentWeapon = null;
    }
}
