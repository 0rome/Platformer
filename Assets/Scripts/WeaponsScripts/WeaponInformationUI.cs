using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInformationUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponDamage;
    [SerializeField] private TextMeshProUGUI weaponAttackSpeed;
    [SerializeField] private TextMeshProUGUI maxAmmo;


    private Weapon weapon;
    private RangeWeapon rangeWeapon;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();

        rangeWeapon = GetComponent<RangeWeapon>();

        if (TryGetComponent(out rangeWeapon))
        {
            if (gameObject.tag == "RangeWeapon")
            {
                maxAmmo.text = "ammo          " + rangeWeapon.Ammo.ToString();
            }
            else
            {
                maxAmmo.gameObject.SetActive(false);
            }
        }
        else
        {
            maxAmmo.gameObject.SetActive(false);
        }

        weaponImage.sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        weaponAttackSpeed.text = weapon.AttackSpeed.ToString();

        weaponName.text = name;
        weaponDamage.text = weapon.Damage.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            Enable();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Disable();
        }
    }
    public void Disable()
    {
        canvas.SetActive(false);
    }
    public void Enable()
    {
        canvas.SetActive(true);
    }
}
