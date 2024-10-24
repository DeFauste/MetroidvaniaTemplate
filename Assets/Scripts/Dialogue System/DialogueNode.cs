using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string optionText;
    public string nextNodeId;
}

[System.Serializable]
public class DialogueNode
{
    public string id;
    public string characterName;
    [TextArea(3, 10)]
    public string text;
    public List<DialogueOption> options;
}
