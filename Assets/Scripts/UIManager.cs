using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public CanvasGroup startScreenCanvasGroup;
    private CanvasGroup[] allScreens;

    void Awake()
    {
        allScreens = new CanvasGroup[] { startScreenCanvasGroup };
    }

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

    public void ShowStartScreen() => ShowOnly(startScreenCanvasGroup);
    public void HideStartScreen() => CanvasGroupDisplayer.Hide(startScreenCanvasGroup);

    public bool IsOnStartScreen => startScreenCanvasGroup != null && startScreenCanvasGroup.alpha > 0f;

    // Scene switching
    public void LoadCookingScene() => SceneManager.LoadScene("CookingScene");
    public void LoadServingScene() => SceneManager.LoadScene("ServingScene");
    public void LoadOutsideScene() => SceneManager.LoadScene("OutsideScene");
}