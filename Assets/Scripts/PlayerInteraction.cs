using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteraction : MonoBehaviour
{
    public UIManager uiManager;
    private PlayerInput input;
    private string currentZone = "";

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        bool isInputAllowed = !(PauseManager.Instance?.IsPaused ?? false) && !(uiManager?.IsOnStartScreen ?? false);

        if (!isInputAllowed)
            return;

        if (input.InteractPressed && currentZone != "")
        {
            EnterZone(currentZone);
        }
    }

    void EnterZone(string zone)
    {
        switch(zone)
        {
            case "Cooking":
                uiManager.LoadCookingScene();
                break;
            case "Serving":
                uiManager.LoadServingScene();
                break;
            case "Exit":
                uiManager.LoadOutsideScene();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooking"))
            currentZone = "Cooking";
        else if (collision.CompareTag("Serving"))
            currentZone = "Serving";
        else if (collision.CompareTag("Exit"))
            currentZone = "Exit";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooking") || collision.CompareTag("Serving") || collision.CompareTag("Exit"))
            currentZone = "";
    }
}