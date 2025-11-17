using UnityEngine;
using Ink.Runtime;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink Story to Play")]
    public TextAsset inkJSON;

    private bool dialogueStarted = false;

    private void Start()
    {
        // Automatically start dialogue when the scene loads
        StartDialogue();
    }

    public void StartDialogue()
    {
        if (dialogueStarted || inkJSON == null) return;

        dialogueStarted = true;
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}