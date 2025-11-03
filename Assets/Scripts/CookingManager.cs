using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public GameObject recipeBookScreen;
    public Transform ramenBowl;
    public Transform noodlePot;
    
    private bool isSpicy = false;

    public enum RamenStep
    {
        AddNoodles,
        AddBroth,
        AddToppings,
        Completed
    }

    [HideInInspector]
    public RamenStep currentStep = RamenStep.AddNoodles;

    // Track added ingredients
    private List<Ingredient.IngredientName> currentToppings = new List<Ingredient.IngredientName>();
    private bool hasEgg = false; // All ramen must include egg
    private Ingredient.IngredientName selectedNoodles = Ingredient.IngredientName.None;
    private Ingredient.IngredientName selectedBroth = Ingredient.IngredientName.None;


    void Start()
    {
        recipeBookScreen.SetActive(false);
        currentStep = RamenStep.AddNoodles;
    }

    public void ShowRecipeBook() => recipeBookScreen.SetActive(true);
    public void HideRecipeBook() => recipeBookScreen.SetActive(false);

    public bool TryAddIngredient(GameObject ingredient)
    {
        Ingredient ing = ingredient.GetComponent<Ingredient>();
        if (ing == null)
        {
            Debug.LogWarning("No Ingredient script found on " + ingredient.name);
            return false;
        }

        switch (currentStep)
        {
            case RamenStep.AddNoodles:
                if (ing.type == Ingredient.IngredientType.Noodles)
                {
                    DraggableObject noodle = ingredient.GetComponent<DraggableObject>();
                    if (noodle.noodleState == DraggableObject.NoodleState.Cooked)
                    {
                        SnapIngredient(ingredient);
                        selectedNoodles = ing.nameID; // track noodle type
                        Debug.Log($"Added {selectedNoodles}! Now add the broth.");
                        currentStep = RamenStep.AddBroth;
                        return true;
                    }
                    Debug.Log("Noodles are not cooked yet!");
                    return false;
                }
                break;

            case RamenStep.AddBroth:
                if (ing.type == Ingredient.IngredientType.Broth)
                {
                    SnapIngredient(ingredient);
                    selectedBroth = ing.nameID; // Track the broth type
                    Debug.Log($"Broth added: {selectedBroth}. Add your toppings.");
                    currentStep = RamenStep.AddToppings;
                    return true;
                }
                break;

            case RamenStep.AddToppings:
                if (ing.type == Ingredient.IngredientType.Topping)
                {
                    SnapIngredient(ingredient);

                    if (!currentToppings.Contains(ing.nameID))
                        currentToppings.Add(ing.nameID);

                    if (ing.nameID == Ingredient.IngredientName.Egg)
                        hasEgg = true;

                    Debug.Log($"Topping added: {ing.nameID}");
                    return true;
                }
                break;

            case RamenStep.Completed:
                Debug.Log("Ramen already completed!");
                return false;
        }

        Debug.Log($"Cannot add {ing.type} yet!");
        return false;
    }

    private void SnapIngredient(GameObject ingredient)
    {
        ingredient.transform.position = ramenBowl.position;
        ingredient.transform.SetParent(ramenBowl);
    }

    public void FinishRamen()
    {
        if (currentStep != RamenStep.AddToppings)
        {
            Debug.Log("You haven't added all ramen components yet!");
            return;
        }

        bool isValid = CheckRamenRecipe();

        if (isValid)
        {
            currentStep = RamenStep.Completed;
            Debug.Log("Ramen completed!");

            if (OrderManager.Instance != null && OrderManager.Instance.HasActiveOrder)
            {
                OrderManager.Instance.CompleteOrder(selectedBroth, selectedNoodles, isSpicy);
            }
            else
            {
                Debug.Log("No active order to complete!");
            }
        }
        else
        {
            Debug.Log("Current toppings: " + string.Join(", ", currentToppings));
            Debug.Log("Invalid ramen, you can keep adding toppings!");
        }
    }

    private bool CheckRamenRecipe()
    {
        isSpicy = currentToppings.Contains(Ingredient.IngredientName.ChiliOil);

        Debug.Log("Current toppings: " + string.Join(", ", currentToppings));
        
        if (selectedNoodles == Ingredient.IngredientName.None)
        {
            Debug.Log("You need to add noodles!");
            return false;
        }
        
        if (selectedBroth == Ingredient.IngredientName.None)
        {
            Debug.Log("You need to add a broth!");
            return false;
        }
        
        if (!hasEgg)
        {
            Debug.Log("Every ramen needs an egg!");
            return false;
        }

        // Define topping sets
        var tonkotsuToppings = new HashSet<Ingredient.IngredientName>
        {
            Ingredient.IngredientName.Chashu,
            Ingredient.IngredientName.WoodEarMushrooms,
            Ingredient.IngredientName.GreenOnion,
            Ingredient.IngredientName.BeanSprout
        };

        var shoyuToppings = new HashSet<Ingredient.IngredientName>
        {
            Ingredient.IngredientName.Kakuni,
            Ingredient.IngredientName.Nori,
            Ingredient.IngredientName.GreenOnion,
            Ingredient.IngredientName.BambooShoots
        };

        var misoToppings = new HashSet<Ingredient.IngredientName>
        {
            Ingredient.IngredientName.Chashu,
            Ingredient.IngredientName.Corn,
            Ingredient.IngredientName.GreenOnion
        };

        var added = new HashSet<Ingredient.IngredientName>(currentToppings);

        // Check combination of broth + toppings
        switch (selectedBroth)
        {
            case Ingredient.IngredientName.TonkotsuBroth:
                if (added.IsSupersetOf(tonkotsuToppings))
                {
                    Debug.Log($"You made Tonkotsu Ramen with {selectedNoodles}!");
                    return true;
                }
                break;

            case Ingredient.IngredientName.ShoyuBroth:
                if (added.IsSupersetOf(shoyuToppings))
                {
                    Debug.Log($"You made Shoyu Ramen with {selectedNoodles}!");
                    return true;
                }
                break;

            case Ingredient.IngredientName.MisoBroth:
                if (added.IsSupersetOf(misoToppings))
                {
                    isSpicy = added.Contains(Ingredient.IngredientName.ChiliOil);

                    if (isSpicy)
                        Debug.Log($"You made Spicy Miso Ramen with {selectedNoodles}!");
                    else
                        Debug.Log($"You made Miso Ramen with {selectedNoodles}!");
                    return true;
                }
                break;

        }

        Debug.Log("The toppings don't match the broth type!");
        return false;
    }


    // ---- Cooking logic remains unchanged ----

    public void StartCookingNoodles(GameObject noodle)
    {
        DraggableObject draggable = noodle.GetComponent<DraggableObject>();
        if (draggable.noodleState != DraggableObject.NoodleState.Raw) return;

        draggable.noodleState = DraggableObject.NoodleState.Cooking;
        draggable.activeCooking = StartCoroutine(CookNoodlesCoroutine(draggable));
    }

    private IEnumerator CookNoodlesCoroutine(DraggableObject noodle)
    {
        Debug.Log("Cooking noodles...");

        float cookTime = 5f;
        float elapsed = 0f;

        while (elapsed < cookTime)
        {
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

        if (noodle.transform.parent == noodle.cookingManager.noodlePot)
        {
            noodle.noodleState = DraggableObject.NoodleState.Cooked;
            Debug.Log("Noodles are cooked! Drag them to the bowl.");
        }

        noodle.activeCooking = null;
    }
    
    public void ResetRamen()
    {
        // Reset the step
        currentStep = RamenStep.AddNoodles;

        // Clear toppings and egg flag
        currentToppings.Clear();
        hasEgg = false;
        
        // Clear broth type
        selectedBroth = Ingredient.IngredientName.None;
        
        // clear noodle type
        selectedNoodles = Ingredient.IngredientName.None;

        // Reset all draggable ingredients in the scene
        DraggableObject[] allIngredients = GameObject.FindObjectsByType<DraggableObject>(FindObjectsSortMode.None);
        foreach (var ingredient in allIngredients)
        {
            ingredient.ResetToStart();
        }
        
        // If the ramen was previously “finished”, we want the order to know it’s not completed
        if (OrderManager.Instance != null && OrderManager.Instance.HasActiveOrder)
        {
            // Only unset the completed flag for this order, do not cancel the order itself
            // Call a method in OrderManager to mark the order as incomplete
            OrderManager.Instance.MarkOrderIncomplete();
        }

        Debug.Log("Ramen has been reset. Start over!");
    }


}
