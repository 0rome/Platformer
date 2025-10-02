using UnityEngine;

public class EnemyNeutrality : MonoBehaviour
{
    private bool isNeutral = false;

    public bool IsNeutral { get => isNeutral; set => isNeutral = value; }

    private void Update()
    {
        CheckPlayerWeapons();
    }

    private void CheckPlayerWeapons()
    {
        Collider2D watchRadius = Physics2D.OverlapCircle(transform.position, 20, LayerMask.GetMask("Player"));
        if (watchRadius)
        {
            if (watchRadius.GetComponent<Inventory>().takenWeapons.Count > 0)
            {
                isNeutral = false;
            }
        }
        if (isNeutral == true)
        {
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();

            if (weapon != null)
            {
                weapon.DeactivateWeapon();
            }
        }
        else if (isNeutral == false)
        {
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();

            if (weapon != null)
            {
                weapon.ActivateWeapon();
            }
        }
    }
}
