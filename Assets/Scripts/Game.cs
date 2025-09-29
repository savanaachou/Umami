using UnityEngine;

public class Game : MonoBehaviour
{
    public UIManager uiManager;

    void Start()
    {
        uiManager.ShowStartScreen();
    }

    public void StartGame()
    {
        uiManager.HideStartScreen();
        Debug.Log("Game Started");
    }
}