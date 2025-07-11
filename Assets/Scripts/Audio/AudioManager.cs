using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        CheckKeys();
        StartCoroutine(Cheker());
    }
    IEnumerator Cheker()
    {
        while (true)
        {
            CheckKeys();
            yield return new WaitForSeconds(1f);
        }
    }
    public void CheckKeys()
    {
        // Загружаем сохранённые значения
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
            sfxSlider.value = sfxVolume;
            SetSFXVolume(sfxVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = musicVolume;
            SetMusicVolume(musicVolume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume",musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }
}
