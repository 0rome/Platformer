using UnityEngine;
using DG.Tweening;

public class Gates : MonoBehaviour
{
    private SoundPlay soundPlay;

    private bool isOpened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
    }

    // Update is called once per frame
    public void OpenGates()
    {
        if (!isOpened)
        {
            isOpened = true;
            transform.DOMoveY(transform.position.y + 5, 5f);
            soundPlay.PlaySound(0);
        }
    }
    public void CloseGates()
    {
        if (isOpened)
        {
            isOpened = false;
            transform.DOMoveY(transform.position.y - 5, 5f);
            soundPlay.PlaySound(0);
        }
    }
}
