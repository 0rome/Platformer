using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;  // постоянная скорость вперёд
    [SerializeField] private float sideSpeed = 3f;     // скорость смещения вправо/влево

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // корабль не падает
    }

    void Update()
    {
        // Двигаем объект вперёд (ось Y вперёд в 2D, если нужно ось X — поменяй)
        Vector3 forwardMove = Vector3.up * forwardSpeed * Time.deltaTime;

        // Управление вправо/влево
        float horizontal = Input.GetAxis("Horizontal"); // A/D или стрелки ← →
        Vector3 sideMove = Vector3.right * horizontal * sideSpeed * Time.deltaTime;

        // Применяем движение
        transform.position += forwardMove + sideMove;

        animator.SetFloat("xDir", horizontal, 0.1f, Time.deltaTime);
    }


}
