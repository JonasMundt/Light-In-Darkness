using UnityEngine;
using TMPro;
using System.Collections;

//Automatischer Dialog Logik

public class AutoDialogue : MonoBehaviour
{
    [Header("World Space Objects")]
    public GameObject dialogueBubble;
    public TMP_Text dialogueText;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    public string[] dialogueLines;

    public float lineDuration = 2.5f;

    public bool IsFinished { get; private set; }

    private void Start()
    {
        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);
    }

    public void StartDialogue()
    {
        StartCoroutine(DialogueRoutine());
    }

    private IEnumerator DialogueRoutine()
    {
        IsFinished = false;

        if (dialogueBubble != null)
            dialogueBubble.SetActive(true);

        for (int i = 0; i < dialogueLines.Length; i++)
        {
            if (dialogueText != null)
                dialogueText.text = dialogueLines[i];

            yield return new WaitForSeconds(lineDuration);
        }

        if (dialogueBubble != null)
            dialogueBubble.SetActive(false);

        IsFinished = true;
    }
}