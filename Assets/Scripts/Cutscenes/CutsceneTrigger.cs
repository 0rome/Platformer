using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    private CutsceneManager cutsceneManager;
    private PlayableDirector currentCutscene;
    private bool hasBeenTriggered = false;

    private void Start()
    {
        // Более надежный способ найти CutsceneManager
        if (cutsceneManager == null)
        {
            cutsceneManager = FindFirstObjectByType<CutsceneManager>();
        }

        if (cutsceneManager == null)
        {
            Debug.LogError("CutsceneManager not found!", this);
            return;
        }

        currentCutscene = GetComponent<PlayableDirector>();

        if (currentCutscene == null)
        {
            Debug.LogError("PlayableDirector component not found!", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenTriggered) Destroy(gameObject);

        if (collision.CompareTag("Player"))
        {
            if (cutsceneManager != null && currentCutscene != null)
            {
                cutsceneManager.StartCutscene(currentCutscene);
                hasBeenTriggered = true; // Предотвращаем повторный запуск
            }
        }
    }

    // Опционально: сброс триггера при необходимости
    public void ResetTrigger()
    {
        hasBeenTriggered = false;
    }
}