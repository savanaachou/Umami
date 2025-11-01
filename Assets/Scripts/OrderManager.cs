using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public bool HasActiveOrder { get; private set; }
    public bool IsOrderCompleted { get; private set; }

    // Current order requirements
    public Ingredient.IngredientName RequiredBroth { get; private set; }
    public Ingredient.IngredientName RequiredNoodles { get; private set; }

    // What the player actually made
    public Ingredient.IngredientName CookedBroth { get; private set; }
    public Ingredient.IngredientName CookedNoodles { get; private set; }

    void Awake()
    {
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

        // Randomly pick a broth and noodle type
        RequiredBroth = GetRandomBroth();
        RequiredNoodles = GetRandomNoodles();

        Debug.Log($"New order received: Customer wants {RequiredBroth} Ramen with {RequiredNoodles}!");
    }

    public void CompleteOrder(Ingredient.IngredientName broth, Ingredient.IngredientName noodles)
    {
        if (HasActiveOrder)
        {
            CookedBroth = broth;
            CookedNoodles = noodles;
            IsOrderCompleted = true;
            Debug.Log("Ramen is cooked! Go serve it.");
        }
    }

    public void ServeOrder()
    {
        if (!HasActiveOrder)
        {
            Debug.Log("No order to serve!");
            return;
        }

        if (!IsOrderCompleted)
        {
            Debug.Log("You can’t serve yet, ramen isn’t ready!");
            return;
        }

        // Check if the ramen matches the order
        if (CookedBroth == RequiredBroth && CookedNoodles == RequiredNoodles)
        {
            Debug.Log($"Correct order! {RequiredBroth} Ramen with {RequiredNoodles} served. Customer is happy!");
        }
        else
        {
            Debug.Log($"Wrong order! You served {CookedBroth} Ramen with {CookedNoodles}, but they wanted {RequiredBroth} with {RequiredNoodles}. Customer is unhappy!");
        }

        // Reset order state
        HasActiveOrder = false;
        IsOrderCompleted = false;
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

    private Ingredient.IngredientName GetRandomBroth()
    {
        Ingredient.IngredientName[] broths = {
            Ingredient.IngredientName.TonkotsuBroth,
            Ingredient.IngredientName.ShoyuBroth,
            Ingredient.IngredientName.MisoBroth
        };
        return broths[Random.Range(0, broths.Length)];
    }

    private Ingredient.IngredientName GetRandomNoodles()
    {
        Ingredient.IngredientName[] noodles = {
            Ingredient.IngredientName.CurlyNoodles,
            Ingredient.IngredientName.StraightNoodles
        };
        return noodles[Random.Range(0, noodles.Length)];
    }
}
