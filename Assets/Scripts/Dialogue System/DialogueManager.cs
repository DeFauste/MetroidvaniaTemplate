using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Button optionButtonPrefab;
    [SerializeField] private float spacing = 5f; // Промежуток между кнопками

    private float buttonHeight = 30f; // Высота кнопки
    private DialogueGraph currentDialogue;
    private DialogueNode currentNode;


    void Start()
    {
        dialoguePanel.SetActive(false);
        optionsPanel.SetActive(false);
        buttonHeight = optionButtonPrefab.GetComponent<RectTransform>().rect.height;
    }

    public void StartDialogue(string startNodeId, DialogueGraph dialogueGraph)
    {
        if (dialogueGraph == null)
        {
            return;
        }
        dialogueGraph.Initialize();

        currentDialogue = dialogueGraph;

        currentNode = dialogueGraph.GetNode(startNodeId);
        if (currentNode != null)
        {
            dialoguePanel.SetActive(true);
            DisplayCurrentNode();
        }
    }

    private void DisplayCurrentNode()
    {
        nameText.text = currentNode.characterName;
        dialogueText.text = currentNode.text;

        ShowOptions();
    }

    private void ShowOptions()
    {
        optionsPanel.SetActive(true);

        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentNode.options.Count; i++)
        {
            DialogueOption option = currentNode.options[i];
            Button button = Instantiate(optionButtonPrefab, optionsPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;

            // Устанавливаем позицию кнопки
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -i * (buttonHeight + spacing));

            // Подписка на событие нажатия кнопки
            button.onClick.AddListener(() => SelectOption(option));
        }
    }

    private void SelectOption(DialogueOption option)
    {
        currentNode = currentDialogue.GetNode(option.nextNodeId);

        if (currentNode != null)
        {
            DisplayCurrentNode();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
}