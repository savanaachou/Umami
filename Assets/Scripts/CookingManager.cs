using System.Collections;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public GameObject recipeBookScreen;
    public Transform ramenBowl; // The transform where ingredients snap to
    public Transform noodlePot;  // The pot for cooking noodles

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
                    DraggableObject noodle = ingredient.GetComponent<DraggableObject>();
                    if (noodle.noodleState == DraggableObject.NoodleState.Cooked)
                    {
                        SnapIngredient(ingredient);
                        Debug.Log("Noodles added! Now add the broth.");
                        currentStep = RamenStep.AddBroth;
                        return true;
                    }
                    else
                    {
                        Debug.Log("Noodles are not cooked yet! Put them in the pot first.");
                        return false;
                    }
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

                    if (currentStep != RamenStep.Completed)
                    {
                        currentStep = RamenStep.Completed;

                        if (OrderManager.Instance != null)
                        {
                            if (OrderManager.Instance.HasActiveOrder)
                            {
                                OrderManager.Instance.CompleteOrder();
                            }
                            else
                            {
                                Debug.Log("Toppings added but no active order exists!");
                            }
                        }
                        else
                        {
                            Debug.Log("OrderManager instance not found!");
                        }
                    }

                    return true;
                }
                break;

            case RamenStep.Completed:
                Debug.Log("Ramen is already complete!");
                return false;
        }

        Debug.Log("Cannot add " + ingredientType + " yet! Follow the order.");
        return false;
    }

    private void SnapIngredient(GameObject ingredient)
    {
        ingredient.transform.position = ramenBowl.position; // Snap to bowl
        ingredient.transform.SetParent(ramenBowl);           // Optional: parent to bowl
    }
    
    public void StartCookingNoodles(GameObject noodle)
    {
        DraggableObject draggable = noodle.GetComponent<DraggableObject>();
        if (draggable.noodleState != DraggableObject.NoodleState.Raw) return;

        draggable.noodleState = DraggableObject.NoodleState.Cooking;

        // Track coroutine so we can stop it if noodle is removed
        draggable.activeCooking = StartCoroutine(CookNoodlesCoroutine(draggable));
    }

    private IEnumerator CookNoodlesCoroutine(DraggableObject noodle)
    {
        Debug.Log("Cooking noodles...");

        float cookTime = 10f;
        float elapsed = 0f;

        while (elapsed < cookTime)
        {
            // If noodle was moved out of the pot → cancel cooking
            if (noodle.transform.parent != noodle.cookingManager.noodlePot)
            {
                Debug.Log("Noodle removed from pot, cooking canceled!");
                noodle.noodleState = DraggableObject.NoodleState.Raw;
                noodle.activeCooking = null;
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Only finish if still in pot
        if (noodle.transform.parent == noodle.cookingManager.noodlePot)
        {
            noodle.noodleState = DraggableObject.NoodleState.Cooked;
            Debug.Log("Noodles are cooked! Drag them to the bowl.");
        }

        noodle.activeCooking = null;
    }

}
