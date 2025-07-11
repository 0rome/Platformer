using UnityEngine;

public class AutoSpecialWeapon : AutoShootWeapon
{
    [Header("Special settings")]
    [SerializeField] GameObject specialFire;
    new void Update()
    {
        base.Update(); // Вызываем логику стрельбы из базового класса
        SpecialFire();
    }
    private void SpecialFire()
    {
        if (Input.GetMouseButtonDown(1) && weaponIsActive)
        {
            Debug.Log("SpecialShoot");
        }
    }
}
