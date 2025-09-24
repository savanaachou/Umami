using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    
    private string currentZone = ""; // Track which zone player is in

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement (left/right)
        moveInput = 0f;
        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;
        
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Interaction
        if (Input.GetKeyDown(KeyCode.W) && currentZone != "")
        {
            EnterZone(currentZone);
        }
        
        // if (Input.GetKeyDown(KeyCode.S)) // or Escape
        // {
        //     currentZone = "";
        //     Debug.Log("Exited Zone Manually");
        // }

    }

    void EnterZone(string zone)
    {
        switch(zone)
        {
            case "Cooking":
                Debug.Log("Entering Cooking Area!");
                // Trigger cooking minigame logic here
                break;
            case "Serving":
                Debug.Log("Talking to Customers!");
                // Trigger customer interaction logic here
                break;
            case "Exit":
                Debug.Log("Exiting Shop!");
                // Trigger end-of-day sequence or main menu
                break;
        }
    }

    // Detect when player enters a zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooking"))
        {
            currentZone = "Cooking";
        }
        else if (collision.CompareTag("Serving"))
            currentZone = "Serving";
        else if (collision.CompareTag("Exit"))
            currentZone = "Exit";
    }

    // Detect when player leaves a zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooking") || collision.CompareTag("Serving") || collision.CompareTag("Exit"))
            currentZone = "";
    }
}