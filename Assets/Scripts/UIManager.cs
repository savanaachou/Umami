using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public CanvasGroup startScreenCanvasGroup;
    public CanvasGroup gameScreenCanvasGroup;
    
    // show start screen (hides all other screens)\
    public void ShowStartScreen()
    {
        CanvasGroupDisplayer.Show(startScreenCanvasGroup);
    }

    public void ShowGameScreen()
    {
        CanvasGroupDisplayer.Show(gameScreenCanvasGroup);
    }
    
    public void HideStartScreen()
    {
        CanvasGroupDisplayer.Hide(startScreenCanvasGroup);
    }
    
    public void HideGameScreen()
    {
        CanvasGroupDisplayer.Hide(gameScreenCanvasGroup);
    }

    
}
