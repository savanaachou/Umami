using UnityEngine;

public class Game : MonoBehaviour
{
    public UIManager uiManager;

    void Start()
    {
        // Only the start screen should show at launch
        uiManager.ShowStartScreen();
    }

    public void StartGame()
    {
        // Switch from start screen to game screen
        uiManager.HideStartScreen();
        Debug.Log("Game Started");
    }
}
