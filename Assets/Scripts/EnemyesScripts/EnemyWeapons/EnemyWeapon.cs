using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private EnemyNeutrality enemyNeutrality;
    private void Awake()
    {
        enemyNeutrality = transform.parent.GetComponent<EnemyNeutrality>();
    }
    private void FixedUpdate()
    {
        if (enemyNeutrality != null)
        {
            if (enemyNeutrality.IsNeutral)
            {
                DeactivateWeapon();
            }
            else
            {
                ActivateWeapon();
            }
        }
        
    }
    public void DeactivateWeapon()
    {
        enabled = false;
    }
    public void ActivateWeapon()
    {
        enabled = true;
    }
}
