using System.Collections;
using TMPro;
using UnityEngine;

public class ScreenMessageManager : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float delay = 0.05f; // задержка между буквами
    public AudioSource textTypingSound;

    private Coroutine typingCoroutine;
    
    public void StartTyping(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        textUI.text = "";
        foreach (char c in text)
        {
            textUI.text += c;
            textTypingSound.Play();
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(5f);
        textUI.text = "";
    }
}
