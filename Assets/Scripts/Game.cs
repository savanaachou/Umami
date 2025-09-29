using UnityEngine;

public class Game : MonoBehaviour
{
    public SceneUIManager sceneUiManager;
    private static bool hasGameStarted = false;

    void Start()
    {
        if (!hasGameStarted)
        {
            sceneUiManager.ShowStartScreen();
        }
        else
        {
            sceneUiManager.HideStartScreen();
        }
    }

    public void StartGame()
    {
        hasGameStarted = true; 
        sceneUiManager.HideStartScreen();
        Debug.Log("Game Started");
    }
}