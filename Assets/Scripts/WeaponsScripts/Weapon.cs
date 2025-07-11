using UnityEngine;
using UnityEngine.Assertions.Must;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float attackSpeed;

    protected bool weaponIsActive = false;

    private WeaponFollowCursor weaponFollowCursor;
    private SpriteRenderer weaponSpriteRenderer;
    private BoxCollider2D weaponBoxCollider;
    private Rigidbody2D rb;
    private PolygonCollider2D weaponPhysicsCollider;
    protected WeaponInformationUI weaponInformationUI;


    public int Damage
    {
        get { return damage; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }
    private void Awake()
    {
        weaponFollowCursor = GetComponent<WeaponFollowCursor>();
        weaponSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        weaponBoxCollider = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        if (TryGetComponent(out weaponPhysicsCollider)) { weaponPhysicsCollider.GetComponent<PolygonCollider2D>(); }
    }


    public virtual void TakeGun()
    {
        weaponPhysicsCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;

        weaponFollowCursor.enabled = true;
        weaponIsActive = true;
        weaponBoxCollider.enabled = true;
        weaponSpriteRenderer.enabled = true;
        enabled = true;
    }
    public virtual void ThrowGun()
    {
        weaponPhysicsCollider.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        weaponFollowCursor.enabled = false;
        weaponIsActive = false;
    }
    public virtual void DeActiveGun()
    {
        weaponFollowCursor.enabled = false;
        weaponIsActive = false;
        weaponSpriteRenderer.enabled = false;
        weaponBoxCollider.enabled = false;
        enabled = false;
    }
}
