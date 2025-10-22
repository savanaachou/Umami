using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public bool HasActiveOrder { get; private set; }
    public bool IsOrderCompleted { get; private set; }

    void Awake()
    {
        // Singleton pattern (keep one copy across scenes)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TakeOrder()
    {
        HasActiveOrder = true;
        IsOrderCompleted = false;
        Debug.Log("New order received: Customer wants ramen!");
    }

    public void CompleteOrder()
    {
        if (HasActiveOrder)
        {
            IsOrderCompleted = true;
            Debug.Log("Ramen is cooked! Go serve it.");
        }
    }

    public void ServeOrder()
    {
        if (HasActiveOrder && IsOrderCompleted)
        {
            Debug.Log("Order served successfully! Customer is happy.");
            HasActiveOrder = false;
            IsOrderCompleted = false;
        }
        else if (HasActiveOrder && !IsOrderCompleted)
        {
            Debug.Log("You can’t serve yet, ramen isn’t ready!");
        }
        else
        {
            Debug.Log("No order to serve!");
        }
    }
    
    public void ResetOrder()
    {
        HasActiveOrder = false;
        IsOrderCompleted = false;
        Debug.Log("Order reset. Ready for a new order!");
    }
    
    public void MarkOrderIncomplete()
    {
        if (HasActiveOrder)
        {
            IsOrderCompleted = false;
            Debug.Log("Ramen marked as incomplete. Finish it again to serve!");
        }
    }

    
}