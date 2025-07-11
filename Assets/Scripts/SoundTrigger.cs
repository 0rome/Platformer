using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip soundClip; // ��������� ��� ����� ��������
    [Range(0.5f, 2f)] public float pitch = 1f; // ��������� pitch (�� 0.5 �� 2)
    private static AudioSource audioSource; // ����� ����������
    private static AudioClip currentClip; // ������� ������������� ����

    void Start()
    {
        // ������� ����� AudioSource, ���� ��� ��� ���
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("GlobalAudioSource");
            audioSource = audioObj.AddComponent<AudioSource>();
            audioSource.loop = true; // ����������� ����
            //DontDestroyOnLoad(audioObj); // ��������� ���� ��� ����� ����
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ���������, ��� ��� �����
        {
            if (audioSource.isPlaying && audioSource.clip == soundClip)
            {
                return; // ���� ���� ��� ������, �� ������������� ���
            }

            // ������������� ���������� ���� � ��������� ����� � ��������� pitch
            audioSource.Stop();
            audioSource.clip = soundClip;
            audioSource.pitch = pitch; // ��������� pitch
            audioSource.Play();
            currentClip = soundClip;
        }
    }
}
