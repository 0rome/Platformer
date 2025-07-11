using UnityEngine;

public class ParallaxTrigger : MonoBehaviour
{
    public int backgroundIndex; // ������ ������ ����
    private ParallaxManager parallaxManager;

    void Start()
    {
        parallaxManager = FindFirstObjectByType<ParallaxManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parallaxManager.ChangeBackground(backgroundIndex);
        }
    }
}
