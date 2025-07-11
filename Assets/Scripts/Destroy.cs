using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
