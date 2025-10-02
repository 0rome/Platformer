using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    private PlayableDirector activeCutscene;
    private Player player;

    private Enemy[] allEnemyes;
    private Traps[] allTraps;

    private void Awake()
    {
        allEnemyes = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        allTraps = FindObjectsByType<Traps>(FindObjectsSortMode.None);
    }

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    public void StartCutscene(PlayableDirector cutscene)
    {
        if (activeCutscene != null && activeCutscene.state == PlayState.Playing)
        {
            activeCutscene.Stop();
        }

        player.DeactivatePlayer();

        activeCutscene = cutscene;

        foreach (var enemy in allEnemyes)
        {
            enemy.DeactivateEnemy();
        }
        foreach (var traps in allTraps)
        {
            traps.gameObject.SetActive(false);
        }

        // Подписываемся на событие окончания катсцены
        activeCutscene.stopped += OnCutsceneFinished;
        activeCutscene.Play();
    }

    public void StopCurrentCutscene()
    {
        if (activeCutscene != null)
        {
            // Отписываемся от события перед остановкой
            activeCutscene.stopped -= OnCutsceneFinished;
            activeCutscene.Stop();
            activeCutscene = null;
        }
    }

    // Событие окончания катсцены
    private void OnCutsceneFinished(PlayableDirector director)
    {
        // Проверяем что это именно та катсцена которая должна закончиться
        if (director == activeCutscene)
        {
            Debug.Log("Катсцена завершена!");

            // Вызываем методы после катсцены
            OnCutsceneEnd();

            // Отписываемся от события
            activeCutscene.stopped -= OnCutsceneFinished;
            activeCutscene = null;
        }
    }

    private void OnCutsceneEnd()
    {
        // Включить управление игроком
        if (player != null)
        {
            player.ActivatePlayer();
        }
        foreach (var enemy in allEnemyes)
        {
            enemy.ActivateEnemy();
        }
        foreach (var traps in allTraps)
        {
            traps.gameObject.SetActive(true);
        }
        // Дополнительные действия после катсцены
        // Например: включить UI, запустить следующую сцену и т.д.
    }
}