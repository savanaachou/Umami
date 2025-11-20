using UnityEngine;

public class NightManager : MonoBehaviour
{
    public static NightManager Instance;

    [Header("Nights in Order")]
    public NightProfile[] nights;

    public int currentNightIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public TextAsset GetCurrentInk()
    {
        return nights[currentNightIndex].inkJSON;
    }

    public bool AdvanceToNextNight()
    {
        if (currentNightIndex + 1 < nights.Length)
        {
            currentNightIndex++;
            return true;
        }

        Debug.Log("No more nights. Game finished!");
        return false;
    }
}