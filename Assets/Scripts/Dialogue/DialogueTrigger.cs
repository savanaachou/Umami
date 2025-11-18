using UnityEngine;
using Ink.Runtime;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink Story to Play")]
    public TextAsset inkJSON;

    // Called by a button
    public void StartDialogue()
    {
        if (inkJSON == null)
        {
            Debug.LogError("No Ink JSON assigned to DialogueTrigger!");
            return;
        }

        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}