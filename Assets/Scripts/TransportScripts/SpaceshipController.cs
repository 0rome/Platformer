using UnityEngine;
using UnityEngine.UIElements;

public class SpaceshipController : Transport
{
    [Header("Movement Settings")]
    public float thrust = 5f; // Сила ускорения
    public float rotationSpeed = 100f; // Скорость поворота

    [Header("Thrusters")]
    [SerializeField] private ParticleSystem[] leftThrusters; // Левые Particle System
    [SerializeField] private ParticleSystem[] rightThrusters; // Правые Particle System

    private SoundPlay soundPlay;
    private Rigidbody2D rb;
    private bool isMoving;

    void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Controll();
    }

    public override void Controll()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!isMoving)
            {
                isMoving = true;
                soundPlay.PlaySound(0);
            }

            rb.AddForce(transform.up * thrust * Time.deltaTime); // Применяем силу для движения вперёд
        }
        else if (isMoving)
        {
            isMoving = false;
            soundPlay.StopSound(0);
        }
    }

    private void HandleRotation()
    {
        float rotation = 0;

        if (Input.GetKey(KeyCode.A))
        {
            rotation += rotationSpeed;
            ToggleThrusters(rightThrusters, true);
        }
        else
        {
            ToggleThrusters(rightThrusters, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotation -= rotationSpeed;
            ToggleThrusters(leftThrusters, true);
        }
        else
        {
            ToggleThrusters(leftThrusters, false);
        }

        // Применяем поворот
        rb.rotation += rotation * Time.deltaTime;
    }

    private void ToggleThrusters(ParticleSystem[] thrusters, bool activate)
    {
        foreach (var thruster in thrusters)
        {
            if (activate && !thruster.isPlaying)
            {
                thruster.Play();
            }
            else if (!activate && thruster.isPlaying)
            {
                thruster.Stop();
            }
        }
    }
}
