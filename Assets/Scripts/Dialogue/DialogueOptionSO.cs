using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueOption", menuName = "Dialogue/Option")]
public class DialogueOptionSO : ScriptableObject
{
    public string optionText;
    public bool isCorrect;
    public DialogueNodeSO nextNode; // ссылка на следующий узел
}
