using DG.Tweening;
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField] private float forceAmount = 10f;

    private SoundPlay soundPlay;

    private Rigidbody2D rb;
    private bool canJump;


    void Start()
    {
        // Двигаем объект по оси X на 5 единиц за 1 секунду
        transform.DOScale(0.7f, 0.3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        soundPlay = GetComponent<SoundPlay>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb = collision.GetComponent<Rigidbody2D>();
            canJump = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb = null;
            canJump = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyBindings.GetKey("Jump")) && canJump && rb != null)
        {
            soundPlay.PlaySound(0);
            Vector2 jumpDir = transform.up;
            rb.AddForce(jumpDir.normalized * forceAmount, ForceMode2D.Impulse);
            transform.DOScale(1.2f, 0.1f);
        }
    }
}
