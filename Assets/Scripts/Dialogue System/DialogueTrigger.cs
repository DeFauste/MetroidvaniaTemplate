using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueGraph dialogueGraph;
    [SerializeField] private string startNodeId;
	[SerializeField] private DialogueManager dialogueManager;

    private bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void StartDialogue()
    {
        if (dialogueManager != null && dialogueGraph != null)
        {
            dialogueManager.StartDialogue(startNodeId, dialogueGraph);
        }
    }
}