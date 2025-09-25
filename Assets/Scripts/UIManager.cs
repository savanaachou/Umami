using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup startScreenCanvasGroup;
    public CanvasGroup gameScreenCanvasGroup;
    public CanvasGroup pauseScreenCanvasGroup;
    public CanvasGroup cookingScreenCanvasGroup;
    public CanvasGroup servingScreenCanvasGroup;
    public CanvasGroup outsideScreenCanvasGroup;

    private CanvasGroup[] allScreens;

    void Awake()
    {
        // Store all screens for easy iteration
        allScreens = new CanvasGroup[] {
            startScreenCanvasGroup,
            gameScreenCanvasGroup,
            pauseScreenCanvasGroup,
            cookingScreenCanvasGroup,
            servingScreenCanvasGroup,
            outsideScreenCanvasGroup
        };
    }

    // Hides all screens, then shows the one passed in.
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

    // Convenience Show Methods
    public void ShowStartScreen() => ShowOnly(startScreenCanvasGroup);
    public void ShowGameScreen() => ShowOnly(gameScreenCanvasGroup);
    public void ShowPauseScreen() => ShowOnly(pauseScreenCanvasGroup);
    public void ShowCookingScreen() => ShowOnly(cookingScreenCanvasGroup);
    public void ShowServingScreen() => ShowOnly(servingScreenCanvasGroup);
    public void ShowOutsideScreen() => ShowOnly(outsideScreenCanvasGroup);

    // Hide Methods (for buttons etc.)
    public void HideStartScreen() => CanvasGroupDisplayer.Hide(startScreenCanvasGroup);
    public void HideGameScreen() => CanvasGroupDisplayer.Hide(gameScreenCanvasGroup);
    public void HidePauseScreen() => CanvasGroupDisplayer.Hide(pauseScreenCanvasGroup);
    public void HideCookingScreen() => CanvasGroupDisplayer.Hide(cookingScreenCanvasGroup);
    public void HideServingScreen() => CanvasGroupDisplayer.Hide(servingScreenCanvasGroup);
    public void HideOutsideScreen() => CanvasGroupDisplayer.Hide(outsideScreenCanvasGroup);
}
