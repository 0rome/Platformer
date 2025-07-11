using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseManager : MonoBehaviour
{
    public static GamePauseManager Instance { get; private set; }

    [SerializeField] private GameObject Menu;
    private bool isPaused = false;

    private Animator animator;
    private SceneController sceneController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Объект не удаляется при смене сцены
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликаты
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
        
    }
    public void LoadMenuScene()
    {
        animator.SetTrigger("pauseOff");
        isPaused = !isPaused;
        sceneController = FindAnyObjectByType<SceneController>();
        sceneController.LoadLevel(0);
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            //PauseGame();
            //if (Menu != null) Menu.SetActive(true);
            animator.SetTrigger("pauseOn");
        }
        else
        {
            //ResumeGame();
            //if (Menu != null) Menu.SetActive(false);
            animator.SetTrigger("pauseOff");
        }
    }

    private void PauseGame()
    {
        // Останавливаем всех врагов
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            enemy.enabled = false;
            if (enemy.TryGetComponent(out Animator anim)) anim.speed = 0;
            if (enemy.TryGetComponent(out Rigidbody2D rb)) rb.simulated = false;
        }
        foreach (Player player in FindObjectsByType<Player>(FindObjectsSortMode.None))
        {
            //player.enabled = false;
            player.DeactivatePlayer();

            if (player.TryGetComponent(out Animator anim)) anim.speed = 0;
            if (player.TryGetComponent(out Rigidbody2D rb)) rb.simulated = false;
        }
        foreach (Boss boss in FindObjectsByType<Boss>(FindObjectsSortMode.None))
        {
            boss.enabled = false;
            if (boss.TryGetComponent(out Animator anim)) anim.speed = 0;
            if (boss.TryGetComponent(out Rigidbody2D rb)) rb.simulated = false;
        }
        foreach (Weapon weapon in FindObjectsByType<Weapon>(FindObjectsSortMode.None))
        {
            weapon.enabled = false;
            if (weapon.TryGetComponent(out Animator anim)) anim.speed = 0;
            if (weapon.TryGetComponent(out Rigidbody2D rb)) rb.simulated = false;
        }
    }

    private void ResumeGame()
    {
        // Включаем
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            enemy.enabled = true;
            if (enemy.TryGetComponent(out Animator anim)) anim.speed = 1;
            if (enemy.TryGetComponent(out Rigidbody2D rb)) rb.simulated = true;
        }
        foreach (Player player in FindObjectsByType<Player>(FindObjectsSortMode.None))
        {
            //player.enabled = true;
            player.ActivatePlayer();

            if (player.TryGetComponent(out Animator anim)) anim.speed = 1;
            if (player.TryGetComponent(out Rigidbody2D rb)) rb.simulated = true;
        }
        foreach (Boss boss in FindObjectsByType<Boss>(FindObjectsSortMode.None))
        {
            boss.enabled = true;
            if (boss.TryGetComponent(out Animator anim)) anim.speed = 1;
            if (boss.TryGetComponent(out Rigidbody2D rb)) rb.simulated = true;
        }
        foreach (Weapon weapon in FindObjectsByType<Weapon>(FindObjectsSortMode.None))
        {
            weapon.enabled = false;
            if (weapon.TryGetComponent(out Animator anim)) anim.speed = 1;
            if (weapon.TryGetComponent(out Rigidbody2D rb)) rb.simulated = true;
        }
    }
}
