using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private string currentZone = "";  // Keeps track of which zone player is in

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // Press E to interact
        {
            if (currentZone == "Cooking")
                StartCooking();
            else if (currentZone == "Serving")
                ServeCustomer();
            else if (currentZone == "Exit")
                ExitShop();
        }
    }

    void StartCooking()
    {
        Debug.Log("Started cooking!");
        // Trigger cooking mini-game here
    }

    void ServeCustomer()
    {
        Debug.Log("Serving customer!");
        // Trigger dialogue / order system here
    }

    void ExitShop()
    {
        Debug.Log("Exiting shop!");
        // Trigger end-of-day or main menu transition
    }

    // Detect entering/exiting zones
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CookingZone"))
            currentZone = "Cooking";
        else if (other.CompareTag("ServingZone"))
            currentZone = "Serving";
        else if (other.CompareTag("ExitZone"))
            currentZone = "Exit";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CookingZone") || other.CompareTag("ServingZone") || other.CompareTag("ExitZone"))
            currentZone = "";
    }
}