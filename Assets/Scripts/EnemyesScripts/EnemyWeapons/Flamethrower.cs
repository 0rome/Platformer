using UnityEngine;

public class Flamethrower : EnemyWeaponAim
{
    [Header("Audio")]
    [SerializeField] private AudioSource flameSound;

    protected override void Fire(Vector2 targetPos)
    {
        if (shootEffect != null && !shootEffect.isPlaying)
            shootEffect.Play();

        if (flameSound != null && !flameSound.isPlaying)
        {
            flameSound.loop = true;
            flameSound.Play();
        }
    }

    protected override void StopFire()
    {
        if (shootEffect != null && shootEffect.isPlaying)
            shootEffect.Stop();

        if (flameSound != null && flameSound.isPlaying)
            flameSound.Stop();
    }
}
