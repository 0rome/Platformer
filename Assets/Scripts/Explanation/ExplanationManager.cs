using UnityEngine;

 public class ExplanationManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Explanations;

    public void ShowExplanation(int explanationIndex)
    {
        Explanations[explanationIndex].SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseExplanation()
    {
        Time.timeScale = 1;

        foreach (var obj in Explanations)
        {
            if (obj != null) obj.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsAnyActive(Explanations))
        {
            CloseExplanation();
        }
    }
    private bool IsAnyActive(GameObject[] array)
    {
        foreach (GameObject obj in array)
        {
            if (obj != null && obj.activeInHierarchy) // проверяем в сцене
            {
                return true;
            }
        }
        return false;
    }
}
