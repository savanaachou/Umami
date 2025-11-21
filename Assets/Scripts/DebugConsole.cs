using UnityEngine;
using TMPro;

public class DebugConsole : MonoBehaviour
{
    public static DebugConsole Instance;

    public TextMeshProUGUI consoleText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            consoleText.gameObject.SetActive(!consoleText.gameObject.activeSelf);
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Application.logMessageReceived -= HandleLog;
        }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        consoleText.text = logString;   // <- Only show the most recent log
    }
}