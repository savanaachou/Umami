using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIManager : MonoBehaviour
{
    [Header("Screens)")]
    public CanvasGroup startScreenCanvasGroup;
    public CanvasGroup playerSetupCanvasGroup;
    public CanvasGroup recipeBookCanvasGroup;

    private CanvasGroup[] allScreens;

    void Awake()
    {
        // Collect all CanvasGroups in this scene (only local, not global)
        allScreens = GetComponentsInChildren<CanvasGroup>(true);
    }

    // Show only one screen and hide others
    public void ShowOnly(CanvasGroup screenToShow)
    {
        foreach (var screen in allScreens)
        {
            if (screen != null)
                CanvasGroupDisplayer.Hide(screen);
        }

        if (screenToShow != null)
            CanvasGroupDisplayer.Show(screenToShow);
    }

    public void ShowStartScreen()
    {
        if (startScreenCanvasGroup != null)
            ShowOnly(startScreenCanvasGroup);
    }

    public void HideStartScreen()
    {
        if (startScreenCanvasGroup != null)
            CanvasGroupDisplayer.Hide(startScreenCanvasGroup);
    }
    
    public void ShowPlayerSetupScreen()
    {
        if (playerSetupCanvasGroup != null)
            ShowOnly(playerSetupCanvasGroup);
    }

    public void HidePlayerSetupScreen()
    {
        if (playerSetupCanvasGroup != null)
            CanvasGroupDisplayer.Hide(playerSetupCanvasGroup);
    }

    /*
    public void ShowRecipeBook()
    {
        if(recipeBookCanvasGroup != null)
            ShowOnly(recipeBookCanvasGroup);
    }
    public void HideRecipeBook()
    {
       if (recipeBookCanvasGroup != null)
           CanvasGroupDisplayer.Hide(recipeBookCanvasGroup);
    }
    
    */

    public bool IsOnStartScreen =>
        startScreenCanvasGroup != null &&
        startScreenCanvasGroup.alpha > 0f;
    
    public bool IsOnPlayerSetupScreen =>
        playerSetupCanvasGroup != null &&
        playerSetupCanvasGroup.alpha > 0f;
    
    // =========== Scene transitions ==============
    public void LoadMainScene()
    {
        Time.timeScale = 1f; // reset time
        SceneManager.LoadScene("MainScene");
    }

    public void LoadCookingScene() => SceneManager.LoadScene("CookingScene");
    public void LoadServingScene() => SceneManager.LoadScene("ServingScene");
    public void LoadOutsideScene() => SceneManager.LoadScene("OutsideScene");
}