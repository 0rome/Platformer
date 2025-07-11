using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip soundClip; // Аудиофайл для этого триггера
    [Range(0.5f, 2f)] public float pitch = 1f; // Настройка pitch (от 0.5 до 2)
    private static AudioSource audioSource; // Общий аудиоплеер
    private static AudioClip currentClip; // Текущий проигрываемый звук

    void Start()
    {
        // Создаем общий AudioSource, если его еще нет
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("GlobalAudioSource");
            audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.loop = true; // Зацикливаем звук
            //DontDestroyOnLoad(audioObj); // Оставляем звук при смене сцен
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Проверяем, что это игрок
        {
            if (audioSource.isPlaying && audioSource.clip == soundClip)
            {
                return; // Если звук уже играет, не перезапускаем его
            }

            // Останавливаем предыдущий звук и запускаем новый с изменённым pitch
            audioSource.Stop();
            audioSource.clip = soundClip;
            audioSource.pitch = pitch; // Применяем pitch
            audioSource.Play();
            currentClip = soundClip;
        }
    }
}
