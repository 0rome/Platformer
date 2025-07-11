using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private SoundPlay soundPlay;
    [SerializeField] private GameObject sparksEffect;
    [SerializeField] private Transform sparkEffectTransform;

    private int Damage;

   
    public void SetDamage(int damage)
    {
        Damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyHealth>())
        {
            CameraShake.instance.Shake(0.1f, 0.1f);
            collision.GetComponent<EnemyHealth>().TakeDamage(Damage);
            soundPlay.PlaySound(1);
        }
        if (collision.GetComponent<BossHealth>())
        {
            CameraShake.instance.Shake(0.1f, 0.1f);
            collision.GetComponent<BossHealth>().GetDamage(Damage);
            soundPlay.PlaySound(1);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            CameraShake.instance.Shake(0.1f,0.1f);
            soundPlay.PlaySound(2);

            if (sparksEffect != null || sparkEffectTransform != null)
            {
                Instantiate(sparksEffect, sparkEffectTransform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("У клинка отсутствуют компоненты");
            }
        }
    }
}
