using UnityEngine;

public class ServingButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (!OrderManager.Instance.HasActiveOrder)
        {
            // Take a new order
            OrderManager.Instance.TakeOrder();
        }
        else
        {
            // Try to serve
            OrderManager.Instance.ServeOrder();
        }
    }
}