using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public string ingredientType; // "Noodles", "Broth", "Topping"
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    
    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        var game = Object.FindFirstObjectByType<CookingManager>();
        if (Vector3.Distance(transform.position, game.ramenBowl.position) < 1f)
        {
            bool success = game.TryAddIngredient(gameObject, ingredientType);

            if (!success)
            {
                // Return to original position
                transform.position = startPos;
            }
        }
        else
        {
            // Not over bowl, return to start
            transform.position = startPos;
        }
    }


    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}