using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewDialogueGraph", menuName = "Dialogue/Dialogue Graph")]
public class DialogueGraph : ScriptableObject
{
    [SerializeField] private List<DialogueNode> nodes;

    private Dictionary<string, DialogueNode> nodeDictionary;

    public void Initialize()
    {
        nodeDictionary = new Dictionary<string, DialogueNode>();
        foreach (var node in nodes)
        {
            nodeDictionary[node.id] = node;
        }
    }

    public DialogueNode GetNode(string id)
    {
        if (nodeDictionary.TryGetValue(id, out var node))
        {
            return node;
        }
        return null;
    }
}