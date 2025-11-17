using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Choices UI")] [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    
    private Story currentStory;
    
    private bool dialogueIsPlaying;
    private int? selectedChoiceIndex = null; // nullable, no choice selected initially

    
    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    
    private void Start()
    {
        // dialogueIsPlaying = false;
        // dialoguePanel.SetActive(false);
        
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        // If there are choices AND none is selected, block continuation
        if (currentStory.currentChoices.Count > 0 && !selectedChoiceIndex.HasValue)
        {
            Debug.Log("You must select a choice before continuing!");
            return; // stop here
        }
        
        if (selectedChoiceIndex.HasValue)
        {
            currentStory.ChooseChoiceIndex(selectedChoiceIndex.Value);
            selectedChoiceIndex = null;
        }
        if (currentStory.canContinue)
        {
            // set text for the current idalogue line
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the num of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can suport. Number of choices given: " + currentChoices.Count);
        }
        
        int index = 0;
        // enable and initialize the choices up to the amt of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure theyre hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

    }
    
    public void MakeChoice(int choiceIndex)
    {
        // Just store the choice, don't advance the story yet
        selectedChoiceIndex = choiceIndex;
        
    }
    
    public void OnContinueButtonPressed()
    {
        ContinueStory();
    }

}
