using UnityEngine;
using Ink.Runtime;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink Story to Play")]
    public TextAsset inkJSON;

    // Called by a button
    public void StartDialogue()
    {
        var inkJSON = NightManager.Instance.GetCurrentInk();

        if (inkJSON == null)
        {
            Debug.LogError("No Ink JSON assigned for this night!");
            return;
        }

        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

}