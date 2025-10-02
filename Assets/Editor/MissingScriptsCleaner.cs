using UnityEngine;
using UnityEditor;

public class MissingScriptsCleaner : MonoBehaviour
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts")]
    private static void RemoveMissingScripts()
    {
        int count = 0;

        // перебираем все объекты в сцене
        foreach (GameObject go in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            // фиксит null-скрипты на объекте
            int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            if (removed > 0)
            {
                Debug.Log($"Удалено {removed} missing script(ов) с объекта {go.name}", go);
                count += removed;
            }
        }

        Debug.Log($"Очистка завершена. Всего удалено: {count} missing script(ов).");
    }
}
