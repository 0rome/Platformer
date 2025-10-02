using UnityEngine;

public class SpaceshipLevelManager : MonoBehaviour
{
    private AudioSource levelMusicAudiosource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelMusicAudiosource = GetComponent<AudioSource>();
    }

    private void ReloadLevel()
    {

    }

    private void OnEnable()
    {
        SpaceshipHealth.SpaceshipDead += ReloadLevel;
    }
    private void OnDisable()
    {
        SpaceshipHealth.SpaceshipDead -= ReloadLevel;
    }
}
