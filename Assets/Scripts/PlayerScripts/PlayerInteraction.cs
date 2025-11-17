using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteraction : MonoBehaviour
{
    public SceneUIManager sceneUiManager;
    private PlayerInput input;
    private string currentZone = "";

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        bool isInputAllowed = !(PauseManager.Instance?.IsPaused ?? false)
                              && !(sceneUiManager?.IsOnStartScreen ?? false)
                              && !(sceneUiManager?.IsOnPlayerSetupScreen ?? false); // <-- added

        if (!isInputAllowed)
            return;

        if (input.InteractPressed && currentZone != "")
        {
            EnterZone(currentZone);
        }
    }

    void EnterZone(string zone)
    {
        // Save player’s current position before leaving MainScene
        if (zone != "Main") // only if we’re leaving Main
        {
            var player = Object.FindFirstObjectByType<PlayerMovement>();
            
            if (player != null)
            {
                PlayerData.LastPosition = player.transform.position;
                PlayerData.HasSavedPosition = true;
            }
        }

        switch(zone)
        {
            case "Cooking":
                sceneUiManager.LoadCookingScene();
                break;
            case "Serving":
                sceneUiManager.LoadServingScene();
                break;
            case "Exit":
                sceneUiManager.LoadOutsideScene();
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