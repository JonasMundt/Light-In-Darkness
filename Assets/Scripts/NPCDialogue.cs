using UnityEngine;
using TMPro;

    //NPC Dialog logik

public class NPCDialogue : MonoBehaviour
{
    [Header("World Space Objects")]
    public GameObject dialogueBubble;
    public GameObject interactPrompt;
    public TMP_Text dialogueText;

    //Provisorischer Text -> kann im Inspector geändert werden
    [Header("NPC Text")]
    [TextArea(2, 5)]
    public string npcText = "Ich kann mir diese Plattformen nicht merken...";

    private bool playerInRange = false;
    private bool isDialogueOpen = false;

    private void Start()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (dialogueText != null)
            dialogueText.text = npcText;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueOpen)
            {
                OpenDialogue();
            }
            else
            {
                CloseDialogue();

                if (interactPrompt != null)
                    interactPrompt.SetActive(true);
            }
        }
    }

    //Dialog öffnen
    private void OpenDialogue()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(true);

        if (dialogueText != null)
            dialogueText.text = npcText;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        isDialogueOpen = true;
    }

    //Dialog schließen
    private void CloseDialogue()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        isDialogueOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (!isDialogueOpen && interactPrompt != null)
            interactPrompt.SetActive(true);
    }

    //Dialog schließt sich, wenn man zu weit weg ist
    //Oder anderweitig
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        CloseDialogue();
    }
}