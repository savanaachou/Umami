using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public GameObject recipeBookScreen;
    public Transform ramenBowl; // The transform where ingredients snap to

    public enum RamenStep
    {
        AddNoodles,
        AddBroth,
        AddToppings,
        Completed
    }

    [HideInInspector]
    public RamenStep currentStep = RamenStep.AddNoodles;

    void Start()
    {
        recipeBookScreen.SetActive(false);
        currentStep = RamenStep.AddNoodles;
    }

    public void ShowRecipeBook()
    {
        recipeBookScreen.SetActive(true);
    }

    public void HideRecipeBook()
    {
        recipeBookScreen.SetActive(false);
    }

    // Called when an ingredient is dropped over the bowl
    public bool TryAddIngredient(GameObject ingredient, string ingredientType)
    {
        switch (currentStep)
        {
            case RamenStep.AddNoodles:
                if (ingredientType == "Noodles")
                {
                    SnapIngredient(ingredient);
                    Debug.Log("Noodles added! Now add the broth.");
                    currentStep = RamenStep.AddBroth;
                    return true;
                }
                break;

            case RamenStep.AddBroth:
                if (ingredientType == "Broth")
                {
                    SnapIngredient(ingredient);
                    Debug.Log("Broth added! Add your toppings.");
                    currentStep = RamenStep.AddToppings;
                    return true;
                }
                break;

            case RamenStep.AddToppings:
                if (ingredientType == "Topping")
                {
                    SnapIngredient(ingredient);
                    Debug.Log("Topping added!");
                    return true; // Multiple toppings allowed
                }
                break;

            case RamenStep.Completed:
                Debug.Log("Ramen is already complete!");
                return false;
        }

        Debug.Log("Cannot add " + ingredientType + " yet! Follow the order.");
        return false; // Out-of-order ingredient
    }

    private void SnapIngredient(GameObject ingredient)
    {
        ingredient.transform.position = ramenBowl.position; // Snap to bowl
        ingredient.transform.SetParent(ramenBowl);           // Optional: parent to bowl
    }
}
