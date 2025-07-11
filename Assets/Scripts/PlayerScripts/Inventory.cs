using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   [HideInInspector] public Weapon activeWeapon;
   [HideInInspector] public List<Weapon> takenWeapons = new List<Weapon>();

    public InventoryUI inventoryUI;
    public int maxSlots = 5;

    private Weapon weaponInTrigger;

    private void PickUpWeapon(Weapon weapon)
    {
        if (takenWeapons.Count >= maxSlots)
        {
            Debug.Log("Инвентарь полон!");
            return;
        }

        if (!takenWeapons.Contains(weapon))
        {
            takenWeapons.Add(weapon);
        }
        else
        {
            Debug.Log("Это оружие уже в инвентаре!");
            return;
        }

        if (activeWeapon != null)
        {
            activeWeapon.DeActiveGun();
        }

        activeWeapon = weapon;
        activeWeapon.TakeGun();
        activeWeapon.transform.SetParent(transform);
        activeWeapon.transform.localPosition = Vector3.zero;

        // Проверяем, есть ли обновление UI
        if (inventoryUI != null)
        {
            inventoryUI.SetImages(); // Обновление UI
        }
        else
        {
            Debug.LogError("InventoryUI не назначен в инспекторе!");
        }
    }

    private void ThrowWeapon()
    {
        if (activeWeapon != null)
        {
            activeWeapon.ThrowGun();

            takenWeapons.Remove(activeWeapon);

            activeWeapon.transform.SetParent(null);

            activeWeapon = null;

            if (takenWeapons.Count > 0)
            {
                EquipWeapon(0);
            }

            inventoryUI.SetImages();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyBindings.GetKey("DropWeapon")))
        {
            ThrowWeapon();
        }

        if (weaponInTrigger != null && Input.GetKeyDown(KeyBindings.GetKey("TakeWeapon")))
        {
            PickUpWeapon(weaponInTrigger);
        }

        for (int i = 1; i <= maxSlots; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                EquipWeapon(i - 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Weapon weapon))
        {
            weaponInTrigger = weapon;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Weapon weapon) && weapon == weaponInTrigger)
        {
            weaponInTrigger = null;
        }
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= takenWeapons.Count)
        {
            Debug.Log("Нет оружия в этом слоте!");
            return;
        }

        if (activeWeapon != null)
        {
            activeWeapon.DeActiveGun();
        }

        activeWeapon = takenWeapons[index];
        activeWeapon.TakeGun();

        // Обновляем UI
        inventoryUI.SetImages();
    }
}
