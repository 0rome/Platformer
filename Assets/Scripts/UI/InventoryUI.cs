using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotPrefab;

    void Start()
    {
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            Instantiate(slotPrefab,transform);
        }
        
    }


    public void SetImages()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);
            Image weaponImage = slot.GetChild(0).GetComponent<Image>();
            Image planeImage = transform.GetChild(i).GetComponent<Image>();

            if (i < inventory.takenWeapons.Count)
            {
                Weapon weapon = inventory.takenWeapons[i];
                SpriteRenderer spriteRenderer = weapon.GetComponentInChildren<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    weaponImage.sprite = spriteRenderer.sprite;
                    weaponImage.gameObject.SetActive(true);

                    // Подсветка активного оружия
                    if (weapon == inventory.activeWeapon)
                    {
                        planeImage.color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        planeImage.color = new Color(1, 1, 1, 0.1f);
                    }
                }
                else
                {
                    Debug.LogWarning($"Оружие {weapon.name} не содержит SpriteRenderer!");
                }
            }
            else
            {
                planeImage.color = new Color(1, 1, 1, 0.1f);
                weaponImage.gameObject.SetActive(false);
            }
        }
    }

}
