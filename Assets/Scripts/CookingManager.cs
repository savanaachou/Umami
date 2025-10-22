using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public GameObject recipeBookScreen;
    public Transform ramenBowl;
    public Transform noodlePot;

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
                        Debug.Log("Noodles added! Now add the broth.");
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
                    Debug.Log("Broth added! Add your toppings.");
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
                OrderManager.Instance.CompleteOrder();
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
        Debug.Log("Current toppings: " + string.Join(", ", currentToppings));

        if (!hasEgg)
        {
            Debug.Log("Every ramen needs an egg!");
            return false;
        }

        // Define recipe sets using enums
        var tonkotsu = new HashSet<Ingredient.IngredientName>
        {
            Ingredient.IngredientName.Chashu,
            Ingredient.IngredientName.WoodEarMushrooms,
            Ingredient.IngredientName.GreenOnion,
            Ingredient.IngredientName.BeanSprout
        };

        var shoyu = new HashSet<Ingredient.IngredientName>
        {
            Ingredient.IngredientName.Kakuni,
            Ingredient.IngredientName.Nori,
            Ingredient.IngredientName.GreenOnion,
            Ingredient.IngredientName.BambooShoots
        };

        var miso = new HashSet<Ingredient.IngredientName>
        {
            Ingredient.IngredientName.Chashu,
            Ingredient.IngredientName.Corn,
            Ingredient.IngredientName.GreenOnion
        };

        var added = new HashSet<Ingredient.IngredientName>(currentToppings);

        // Check recipes
        if (added.IsSupersetOf(tonkotsu))
        {
            Debug.Log("You made Tonkotsu Ramen!");
            return true;
        }
        else if (added.IsSupersetOf(shoyu))
        {
            Debug.Log("You made Shoyu Ramen!");
            return true;
        }
        else if (added.IsSupersetOf(miso))
        {
            if (added.Contains(Ingredient.IngredientName.ChiliOil))
                Debug.Log("You made Spicy Miso Ramen!");
            else
                Debug.Log("You made Miso Ramen!");
            return true;
        }
        else
        {
            Debug.Log("The toppings don't match any known recipe!");
            return false;
        }
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
}
