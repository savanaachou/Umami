using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSetupManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField nameInputField;
    public Button maleButton;
    public Button femaleButton;
    public Button nextButton;

    [Header("Highlight Colors")]
    public Color selectedColor = Color.white;
    public Color normalColor = Color.grey;

    [Header("Player Data")]
    public PlayerProfile playerProfile;

    [Header("Game Controller")]
    public Game gameManager;

    private bool isCharacterSelected = false;

    private void Start()
    {
        maleButton.onClick.AddListener(() => ChooseCharacter(true));
        femaleButton.onClick.AddListener(() => ChooseCharacter(false));
        nameInputField.onValueChanged.AddListener(delegate { ValidateSetup(); });
        nextButton.onClick.AddListener(OnNextButtonPressed);

        nextButton.interactable = false;
    }

    private void ChooseCharacter(bool isMale)
    {
        isCharacterSelected = true;
        playerProfile.playerSprite = isMale ? maleButton.image.sprite : femaleButton.image.sprite;
        HighlightSelection(isMale);
        ValidateSetup();

        // Update the actual player sprite immediately
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.UpdatePlayerSprite();
        }
    }

    private void HighlightSelection(bool isMale)
    {
        Image maleImage = maleButton.GetComponent<Image>();
        Image femaleImage = femaleButton.GetComponent<Image>();

        maleImage.color = isMale ? selectedColor : normalColor;
        femaleImage.color = isMale ? normalColor : selectedColor;
    }

    private void ValidateSetup()
    {
        string enteredName = nameInputField.text.Trim();
        bool hasName = !string.IsNullOrEmpty(enteredName);
        nextButton.interactable = hasName && isCharacterSelected;
    }

    private void OnNextButtonPressed()
    {
        playerProfile.playerName = nameInputField.text.Trim();
        Debug.Log($"Player setup complete! Name: {playerProfile.playerName}, Sprite: {playerProfile.playerSprite.name}");

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.UpdatePlayerSprite();
        }
    }
}
