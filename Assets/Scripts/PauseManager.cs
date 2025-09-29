using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public CanvasGroup pauseScreenCanvasGroup;
    private bool isPaused = false;

    public bool IsPaused => isPaused; // read-only property

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (pauseScreenCanvasGroup == null)
                pauseScreenCanvasGroup = GetComponentInChildren<CanvasGroup>();

            HidePauseScreenInstant();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Block pause if start screen is active
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null && uiManager.IsOnStartScreen)
                return;

            TogglePause();
        }
    }


    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ShowPauseScreen();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        HidePauseScreen();
    }

    private void ShowPauseScreen()
    {
        pauseScreenCanvasGroup.alpha = 1f;
        pauseScreenCanvasGroup.interactable = true;
        pauseScreenCanvasGroup.blocksRaycasts = true;
    }

    private void HidePauseScreen()
    {
        pauseScreenCanvasGroup.alpha = 0f;
        pauseScreenCanvasGroup.interactable = false;
        pauseScreenCanvasGroup.blocksRaycasts = false;
    }

    private void HidePauseScreenInstant()
    {
        pauseScreenCanvasGroup.alpha = 0f;
        pauseScreenCanvasGroup.interactable = false;
        pauseScreenCanvasGroup.blocksRaycasts = false;
    }
}