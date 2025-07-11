using Unity.VisualScripting;
using UnityEngine;

public class ScreenMessageTrigger : MonoBehaviour
{
    [SerializeField] private string Message = "Hello world";
    [SerializeField] private bool selfDestruction = true;

    private ScreenMessageManager screenMessageManager;

    void Start()
    {
        screenMessageManager = FindAnyObjectByType<ScreenMessageManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (selfDestruction)
            {
                screenMessageManager.StartTyping(Message);
                Destroy(gameObject);
            }
            else
            {
                screenMessageManager.StartTyping(Message);
            }
        }
    }
}
