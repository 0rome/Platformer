using UnityEngine.Rendering;
using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Volume postProcessingVolume;

    private int timer;

    void Start()
    {
        PostProcessingEffects.Initialize(postProcessingVolume, this);
        StartCoroutine(TimerCoroutine());
    }
    private void Update()
    {
        
    }
    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            timer++;

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name,timer);

            yield return new WaitForSeconds(1);
        }
    }
}
