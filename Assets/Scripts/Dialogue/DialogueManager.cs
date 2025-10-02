using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Transform optionsParent;
    [SerializeField] private GameObject optionButtonPrefab;
    [SerializeField] private Canvas dialogueCanvas;

    [SerializeField] private DialogueNodeSO currentNode; // <-- изменили на SO

    public static event Action OnAllCorrect;
    public static event Action OnAnyWrong;

    private int correctAnswers = 0;
    private int totalQuestions = 0;

    private SoundPlay soundPlay;
    private Player player;

    private Coroutine typingCoroutine;

    private void Start()
    {
        soundPlay = GetComponent<SoundPlay>();
        player = FindAnyObjectByType<Player>();
    }

    public void StartDialogue(DialogueNodeSO startingNode)
    {
        correctAnswers = 0;
        totalQuestions = 0;
        currentNode = startingNode;
        ShowCurrentNode();
        dialogueCanvas.enabled = true;
        player.DeactivatePlayer();
    }

    void ShowCurrentNode()
    {
        characterNameText.text = currentNode.characterName;

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(currentNode.dialogueText));

        foreach (Transform child in optionsParent)
            Destroy(child.gameObject);

        foreach (var option in currentNode.options)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, optionsParent);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => ChooseOption(option));
        }
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            soundPlay.PlaySound(0);
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void ChooseOption(DialogueOptionSO option) // <-- тоже меняем тип
    {
        totalQuestions++;
        if (option.isCorrect)
            correctAnswers++;

        if (option.nextNode != null)
        {
            currentNode = option.nextNode;
            ShowCurrentNode();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (correctAnswers == totalQuestions)
        {
            Debug.Log("Все правильно! Вызов правильного метода.");
            dialogueCanvas.enabled = false;
            player.ActivatePlayer();
            OnAllCorrect?.Invoke();
        }
        else
        {
            Debug.Log("Есть ошибки! Вызов метода ошибки.");
            dialogueCanvas.enabled = false;
            player.ActivatePlayer();
            OnAnyWrong?.Invoke();
        }
    }
}
