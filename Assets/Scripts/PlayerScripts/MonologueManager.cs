using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class MonologueLine
{
    public string text; // Текст реплики
    public AudioClip voiceClip; // Озвучка для реплики (опционально)
}

public class MonologueManager : MonoBehaviour
{
    public GameObject dialoguePanel;            // Панель диалога
    public TextMeshProUGUI dialogueText;        // Текстовая область
    public float typingSpeed = 0.05f;           // Скорость побуквенного вывода текста
    public AudioSource textAudioSource;             // Источник звука
    public AudioSource blipAudioSource;

    public MonologueLine[] monologueLines;      // Массив строк монолога
    private bool isTyping = false;              // Идёт ли сейчас печать текста

    // Показать конкретную строку монолога
    public void ShowLine(int lineIndex)
    {
        if (isTyping || lineIndex < 0 || lineIndex >= monologueLines.Length) return;

        MonologueLine currentLine = monologueLines[lineIndex];
        StartCoroutine(TypeText(currentLine));
    }

    // Показать текст с побуквенным выводом
    private IEnumerator TypeText(MonologueLine line)
    {
        isTyping = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";

        if (line.voiceClip != null)
        {
            textAudioSource.PlayOneShot(line.voiceClip); // Проиграть озвучку
        }

        foreach (char letter in line.text.ToCharArray())
        {
            blipAudioSource.Play();
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        yield return new WaitForSeconds(2f); // Настройте время по вашему желанию
        EndMonologue();
    }

    // Завершение монолога
    public void EndMonologue()
    {
        dialoguePanel.SetActive(false);
    }
}
