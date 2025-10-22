using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 startPos;
    private Vector3 startPosition;
    private Quaternion startRotation;

    public enum NoodleState
    {
        Raw,
        Cooking,
        Cooked
    }
    public NoodleState noodleState = NoodleState.Raw;

    [HideInInspector] public Coroutine activeCooking;
    [HideInInspector] public CookingManager cookingManager;

    private Ingredient ingredient; // reference to Ingredient script

    void Start()
    {
        startPos = transform.position;

        // Save reset position and rotation
        startPosition = transform.position;
        startRotation = transform.rotation;

        cookingManager = Object.FindFirstObjectByType<CookingManager>();
        ingredient = GetComponent<Ingredient>();

        if (ingredient == null)
        {
            Debug.LogWarning("No Ingredient script found on " + gameObject.name);
        }
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;

        // Cancel noodle cooking if being dragged mid-cook
        if (ingredient != null && ingredient.type == Ingredient.IngredientType.Noodles && noodleState == NoodleState.Cooking)
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

        if (ingredient == null)
        {
            transform.position = startPos;
            return;
        }

        // Dropped over bowl
        if (Vector3.Distance(transform.position, game.ramenBowl.position) < 1f)
        {
            bool success = game.TryAddIngredient(gameObject);
            if (!success) transform.position = startPos;
            return;
        }

        // Dropped over pot (for noodles)
        if (ingredient.type == Ingredient.IngredientType.Noodles &&
            Vector3.Distance(transform.position, game.noodlePot.position) < 1f)
        {
            transform.position = game.noodlePot.position;
            transform.SetParent(game.noodlePot);
            game.StartCookingNoodles(gameObject);
            return;
        }

        // Otherwise, return to start position
        transform.position = startPos;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // --- NEW: Reset position and state ---
    public void ResetToStart()
    {
        transform.SetParent(null);
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Reset noodle state if applicable
        if (ingredient != null && ingredient.type == Ingredient.IngredientType.Noodles)
        {
            noodleState = NoodleState.Raw;
            if (activeCooking != null)
            {
                cookingManager.StopCoroutine(activeCooking);
                activeCooking = null;
            }
        }
    }
}
