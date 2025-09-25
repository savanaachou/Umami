using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteraction : MonoBehaviour
{
    public UIManager uiManager;

    private PlayerInput input;
    private string currentZone = "";

    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (input.InteractPressed && currentZone != "")
        {
            EnterZone(currentZone);
        }
    }

    void EnterZone(string zone)
    {
        switch(zone)
        {
            case "Cooking":
                Debug.Log("Entering Cooking Area!");
                uiManager.ShowCookingScreen();
                break;
            case "Serving":
                Debug.Log("Talking to Customers!");
                uiManager.ShowServingScreen();
                break;
            case "Exit":
                Debug.Log("Exiting Shop!");
                uiManager.ShowOutsideScreen();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooking"))
            currentZone = "Cooking";
        else if (collision.CompareTag("Serving"))
            currentZone = "Serving";
        else if (collision.CompareTag("Exit"))
            currentZone = "Exit";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooking") || collision.CompareTag("Serving") || collision.CompareTag("Exit"))
            currentZone = "";
    }
}