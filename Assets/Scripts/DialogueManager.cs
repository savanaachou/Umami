using UnityEngine;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;

    public TMP_Text dialogueText;
    public GameObject choicesContainer;
    public TMP_Text[] choiceTexts;

    void Start()
    {
        story = new Story(inkJSONAsset.text);
        ContinueStory();
    }

    void ContinueStory()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue().Trim();
        }
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            Debug.Log("Story ended!");
        }
    }

    void DisplayChoices()
    {
        for (int i = 0; i < choiceTexts.Length; i++)
        {
            if (i < story.currentChoices.Count)
            {
                choiceTexts[i].text = story.currentChoices[i].text.Trim();
                choiceTexts[i].transform.parent.gameObject.SetActive(true);
            }
            else
            {
                choiceTexts[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public void ChooseOption(int index)
    {
        story.ChooseChoiceIndex(index);
        ContinueStory();
    }
}