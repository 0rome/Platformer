using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    protected PlayerController playerController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            StartMiniGame();
        }
    }
    public abstract void StartMiniGame();
    public abstract void StopMiniGame();

    public abstract void WinMiniGame();
}
