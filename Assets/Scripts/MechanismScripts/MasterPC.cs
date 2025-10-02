using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
public class MasterPC : MonoBehaviour
{
    [SerializeField] private Gates gates;

    [SerializeField] private EnemyHealth[] chasingEnemyes;

    private GameObject sprites;
    private Canvas m_Canvas;
    private SoundPlay soundPlay;
    private Player player;
    private Collider2D triggerCollider;

    private bool badButtonIsPressed;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();

        sprites = transform.Find("Sprites").gameObject;

        soundPlay = GetComponent<SoundPlay>();

        m_Canvas = GetComponentInChildren<Canvas>();

        triggerCollider = GetComponentInChildren<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            collision.GetComponent<PlayerHealth>().SetCheckPoint(transform);

            player.DeactivatePlayer();

            m_Canvas.enabled = true;

        }
    }
    public void CloseMenu()
    {
        m_Canvas.enabled = false;

        player.ActivatePlayer();
    }
    public void RedButton()
    {
        player.ActivatePlayer();

        m_Canvas.enabled = false;

        gates.OpenGates();

        triggerCollider.enabled = false;

        sprites.SetActive(false);

        soundPlay.PlaySound(2);
    }
    public void GrennButton()
    {
        player.ActivatePlayer();

        badButtonIsPressed = true;

        m_Canvas.enabled = false;

        ActivateEnemyes();

        CameraShake.instance.Shake(1,1);

        gates.OpenGates();

        triggerCollider.enabled = false;

        soundPlay.PlaySound(0);
        soundPlay.PlaySound(1);
    }


    private void ActivateEnemyes()
    {
        foreach (var enemy in chasingEnemyes)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    private void RespawnEnemyes()
    {
        StartCoroutine(respawnWithDelay());
    }
    private IEnumerator respawnWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (badButtonIsPressed)
        {
            foreach (var enemy in chasingEnemyes)
            {
                enemy.Respawn();
            }
        }
    }

    private void OnEnable()
    {
        PlayerHealth.OnDead += RespawnEnemyes;
    }
    private void OnDisable()
    {
        PlayerHealth.OnDead -= RespawnEnemyes;
    }
}
