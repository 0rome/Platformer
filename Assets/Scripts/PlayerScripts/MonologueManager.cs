using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class MonologueLine
{
    public string text; // ����� �������
    public AudioClip voiceClip; // ������� ��� ������� (�����������)
}

public class MonologueManager : MonoBehaviour
{
    public GameObject dialoguePanel;            // ������ �������
    public TextMeshProUGUI dialogueText;        // ��������� �������
    public float typingSpeed = 0.05f;           // �������� ������������ ������ ������
    public AudioSource textAudioSource;             // �������� �����
    public AudioSource blipAudioSource;

    public MonologueLine[] monologueLines;      // ������ ����� ��������
    private bool isTyping = false;              // ��� �� ������ ������ ������

    // �������� ���������� ������ ��������
    public void ShowLine(int lineIndex)
    {
        if (isTyping || lineIndex < 0 || lineIndex >= monologueLines.Length) return;

        MonologueLine currentLine = monologueLines[lineIndex];
        StartCoroutine(TypeText(currentLine));
    }

    // �������� ����� � ����������� �������
    private IEnumerator TypeText(MonologueLine line)
    {
        isTyping = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";

        if (line.voiceClip != null)
        {
            textAudioSource.PlayOneShot(line.voiceClip); // ��������� �������
        }

        foreach (char letter in line.text.ToCharArray())
        {
            blipAudioSource.Play();
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        yield return new WaitForSeconds(2f); // ��������� ����� �� ������ �������
        EndMonologue();
    }

    // ���������� ��������
    public void EndMonologue()
    {
        dialoguePanel.SetActive(false);
    }
}
