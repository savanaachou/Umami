using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public SceneUIManager sceneUiManager;
    private static bool hasGameStarted = false;

    void Start()
    {
        if (!hasGameStarted)
        {
            sceneUiManager.ShowStartScreen();
            sceneUiManager.HidePlayerSetupScreen();
        }
        else
        {
            sceneUiManager.HideStartScreen();
            sceneUiManager.HidePlayerSetupScreen();
        }
    }
    
    // Called by Start button on start screen
    public void GoToPlayerSetup()
    {
        sceneUiManager.HideStartScreen();
        sceneUiManager.ShowPlayerSetupScreen();
    }

    public void StartGame()
    {
        hasGameStarted = true; 
        sceneUiManager.HideStartScreen();
        sceneUiManager.HidePlayerSetupScreen();
        Debug.Log("Game Started");
    }
}