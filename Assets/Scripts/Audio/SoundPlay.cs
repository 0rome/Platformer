using UnityEngine;
using UnityEngine.Audio;

public class SoundPlay : MonoBehaviour
{
    public AudioSource[] _audioSources;

    public void PlaySound(int AudioIndex)
    {
        if (AudioIndex <= _audioSources.Length)
        {
            _audioSources[AudioIndex].Play();
        }
        else
        {
            Debug.Log("������ ����� �� ��������� �������");
        }
       
    }
    public void StopSound(int AudioIndex)
    {
        if (AudioIndex <= _audioSources.Length)
        {
            _audioSources[AudioIndex].Stop();
        }
        else
        {
            Debug.Log("������ ����� �� ��������� �������");
        }

    }
}
