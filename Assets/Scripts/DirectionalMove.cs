using UnityEngine;

public class DirectionalMove : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed = 5f;

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
