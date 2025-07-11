using UnityEngine;

public class PlayerController : Player
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed = 5f; // Скорость перемещения
    [SerializeField] private float jumpForce = 10f; // Сила прыжка
    [SerializeField] private float dashSpeed = 15f; // Скорость рывка
    [SerializeField] private float dashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float dashCooldown = 1f; // Время восстановления рывка
    [SerializeField] private LayerMask groundLayer; // Слой для проверки земли

    [Header("Effects")]
    [SerializeField] private ParticleSystem JumpEffect;
    [SerializeField] private ParticleSystem LandingEffect;
    [SerializeField] private ParticleSystem DashEffect;


    private Animator animator; // Компонент Animator
    private Transform groundCheck; // Точка для проверки земли
    private PlayerSounds playerSounds;

    private bool isGrounded; // Находится ли персонаж на земле
    private bool isDashing = false; // В процессе ли рывка
    private bool wasGrounded = false;  
    
    private float dashTime; // Таймер для длительности рывка
    private float groundCheckRadius = 0.2f; // Радиус проверки земли
    private float lastDashTime; // Время последнего рывка

    void Start()
    {
        animator = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        playerSounds = GetComponent<PlayerSounds>();
    }

    void Update()
    {
        if (!isDashing)
        {
            Move();
            Jump();
        }

        Dash();
        UpdateAnimations();

        if (!wasGrounded && isGrounded)
        {
            LandingEffect.Play(); // Воспроизводим эффект приземления
        }

        wasGrounded = isGrounded; // Обновляем предыдущее состояние
    }

    private void Move()
    {
        // Получаем горизонтальный ввод
        float moveInput = 0;

        if (Input.GetKey(KeyBindings.GetKey("MoveLeft")))
            moveInput = -1;
        if (Input.GetKey(KeyBindings.GetKey("MoveRight")))
            moveInput = 1;

        // Устанавливаем скорость движения по горизонтали
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Mathf.Abs(moveInput) > 0.5f && isGrounded)
        {
            playerSounds.PlayFootstepSound();
        }

        // Передаём скорость в анимацию
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    private void Jump()
    {
        // Проверяем, находится ли персонаж на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Прыжок
        if (Input.GetKeyDown(KeyBindings.GetKey("Jump")) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            playerSounds.PlayJumpSound();
            JumpEffect.Play();
        }

        // Передаём состояние прыжка в анимацию
        if (isDashing)
        {
            animator.SetBool("IsJumping", !isGrounded);
        }
        
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyBindings.GetKey("Dash")) && Time.time >= lastDashTime + dashCooldown)
        {
            float moveInput = rb.linearVelocity.x > 0 ? 1 : -1;
            if (Mathf.Abs(moveInput) > 0.01f)
            {
                isDashing = true;
                dashTime = dashDuration;
                lastDashTime = Time.time;
                rb.linearVelocity = new Vector2(moveInput * dashSpeed, rb.linearVelocity.y);
                playerSounds.PlayDashSound();
                DashEffect.Play();

                animator.SetTrigger("Dash");

                rb.excludeLayers += LayerMask.GetMask("Enemy", "EnemyProjectiles","Boss");// добавляем в исключение
            }
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
                rb.excludeLayers -= LayerMask.GetMask("Enemy","EnemyProjectiles","Boss");// убираем из исключения
            }
        }
    }

    private void UpdateAnimations()
    {
        // Если персонаж на земле, сбросить анимацию прыжка
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}