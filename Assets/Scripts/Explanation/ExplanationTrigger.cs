using UnityEngine;

public class ExplanationTrigger : MonoBehaviour
{
    [SerializeField] private int explanationIndex;

    private ExplanationManager explanationManager;

    private void Start()
    {
        explanationManager = FindFirstObjectByType<ExplanationManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            explanationManager.ShowExplanation(explanationIndex);
            Destroy(gameObject);
        }
    }
}
