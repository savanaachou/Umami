using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public string ingredientType; // "Noodles", "Broth", "Topping"
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 startPos;

    public enum NoodleState
    {
        Raw,
        Cooking,
        Cooked
    }
    public NoodleState noodleState = NoodleState.Raw;

    [HideInInspector] public Coroutine activeCooking; // store coroutine reference
    [HideInInspector] public CookingManager cookingManager;

    void Start()
    {
        startPos = transform.position;
        cookingManager = Object.FindFirstObjectByType<CookingManager>();
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;

        // If dragging while cooking → cancel
        if (ingredientType == "Noodles" && noodleState == NoodleState.Cooking)
        {
            if (activeCooking != null)
            {
                cookingManager.StopCoroutine(activeCooking);
                activeCooking = null;
                noodleState = NoodleState.Raw;
                Debug.Log("Cooking canceled! Noodles are raw again.");
            }
        }
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

        var game = cookingManager;

        // Dropped over bowl
        if (Vector3.Distance(transform.position, game.ramenBowl.position) < 1f)
        {
            bool success = game.TryAddIngredient(gameObject, ingredientType);
            if (!success) transform.position = startPos;
            return;
        }

        // Dropped over pot
        if (ingredientType == "Noodles" && Vector3.Distance(transform.position, game.noodlePot.position) < 1f)
        {
            transform.position = game.noodlePot.position; 
            transform.SetParent(game.noodlePot);
            game.StartCookingNoodles(gameObject);
            return;
        }

        // Otherwise return to start
        transform.position = startPos;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
