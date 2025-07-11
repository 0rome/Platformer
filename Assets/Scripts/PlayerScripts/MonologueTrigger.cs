using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [SerializeField] private int monologueIndex;
    [SerializeField] private MonologueManager monologueManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            monologueManager.ShowLine(monologueIndex);
            Destroy(gameObject);
        }
    }
}