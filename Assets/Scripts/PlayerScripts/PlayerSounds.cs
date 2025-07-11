using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource footstepAudio; // �������� ����� �����
    public AudioSource jumpAudio;
    public AudioSource dashAudio;
    // �������� ����� ������

    // ����� ��� ��������������� ����� �����
    public void PlayFootstepSound()
    {
        if (!footstepAudio.isPlaying) // ���������, ����� ���� �� ������������
        {
            footstepAudio.Play();
        }
    }

    // ����� ��� ��������������� ����� ������
    public void PlayJumpSound()
    {
        jumpAudio.Play();
    }
    public void PlayDashSound()
    {
        dashAudio.Play();
    }
}
