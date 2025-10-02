using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private SceneController sceneController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneController = FindFirstObjectByType<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && sceneController != null)
        {
            LoadTransition();
        }
        else
        {
            Debug.Log("SceneController не найден!!!");
        }
    }
    private void LoadTransition()
    {
        sceneController.LoadLevel(sceneName);
    }
}
