using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNodeSO : ScriptableObject
{
    public string characterName;
    [TextArea(3, 10)]
    public string dialogueText;
    public List<DialogueOptionSO> options;
}
