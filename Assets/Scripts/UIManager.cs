using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public CanvasGroup startScreenCanvasGroup;
    public CanvasGroup gameScreenCanvasGroup;
    public CanvasGroup pauseScreenCanvasGroup;
    
    // show start screen (hides all other screens)\
    public void ShowStartScreen()
    {
        CanvasGroupDisplayer.Show(startScreenCanvasGroup);
    }

    public void ShowGameScreen()
    {
        CanvasGroupDisplayer.Show(gameScreenCanvasGroup);
    }

    public void ShowPauseScreen()
    {
        CanvasGroupDisplayer.Show(pauseScreenCanvasGroup);
    }
    
    public void HideStartScreen()
    {
        CanvasGroupDisplayer.Hide(startScreenCanvasGroup);
    }
    
    public void HideGameScreen()
    {
        CanvasGroupDisplayer.Hide(gameScreenCanvasGroup);
    }

    public void HidePauseScreen()
    {
        CanvasGroupDisplayer.Hide(pauseScreenCanvasGroup);
    }

    
}
