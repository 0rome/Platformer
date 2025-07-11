using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource footstepAudio; // Источник звука шагов
    public AudioSource jumpAudio;
    public AudioSource dashAudio;
    // Источник звука прыжка

    // Метод для воспроизведения звука шагов
    public void PlayFootstepSound()
    {
        if (!footstepAudio.isPlaying) // Проверяем, чтобы звук не накладывался
        {
            footstepAudio.Play();
        }
    }

    // Метод для воспроизведения звука прыжка
    public void PlayJumpSound()
    {
        jumpAudio.Play();
    }
    public void PlayDashSound()
    {
        dashAudio.Play();
    }
}
