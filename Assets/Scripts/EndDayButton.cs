using UnityEngine;

public class EndDayButton : MonoBehaviour
{
    public void EndDay()
    {
        // Check DialogueManager first
        DialogueManager dm = DialogueManager.GetInstance();
        if (dm == null)
        {
            Debug.LogError("No DialogueManager instance found!");
            return;
        }

        // Make sure the current story is finished
        if (!dm.StoryFinished)
        {
            Debug.Log("Cannot end the day, finish the dialogue for this night!");
            return;
        }

        // Advance the night safely
        NightManager nm = NightManager.Instance;
        if (nm == null)
        {
            Debug.LogError("No NightManager instance found!");
            return;
        }

        bool advanced = nm.AdvanceToNextNight();
        if (advanced)
        {
            dm.ResetForNewNight();
            Debug.Log("Moved to the next night!");
        }
        else
        {
            Debug.Log("Game finished, no more nights!");
        }
    }
}