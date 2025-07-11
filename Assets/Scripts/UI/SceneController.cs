using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider slider;

    private Animator animator;

    
    private void Start()
    {
        animator = GetComponent<Animator>();

        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            animator.SetTrigger("End");
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    private IEnumerator LoadAsynchronously(int sceneIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(2);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;

            yield return null;
        }
    }
}
