using UnityEngine;

public class Game : MonoBehaviour
{
    
    public UIManager uiManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager.ShowStartScreen();
        uiManager.HideGameScreen();
    }
    
    public void StartGame()
    {
        uiManager.HideStartScreen();
        Debug.Log("Game Started");
    }


}
