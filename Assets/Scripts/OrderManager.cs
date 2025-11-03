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
    
    public bool RequiredIsSpicy { get; private set; }
    public bool CookedIsSpicy { get; private set; }
    
    public float OrderTimer { get; private set; } = 0f; // elapsed time in seconds
    private bool isTiming = false;
    
    void Update()
    {
        if (isTiming && HasActiveOrder && !IsOrderCompleted)
        {
            OrderTimer += Time.deltaTime; // increment by time since last frame
        }
    }

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

        RequiredBroth = GetRandomBroth();
        RequiredNoodles = GetRandomNoodles();

        // Only Miso can be spicy (50% chance)
        if (RequiredBroth == Ingredient.IngredientName.MisoBroth)
        {
            RequiredIsSpicy = Random.value > 0.5f;
        }
        else
        {
            RequiredIsSpicy = false;
        }
        
        // Start the timer
        OrderTimer = 0f;
        isTiming = true;
        Debug.Log("Timer Started");

        string spiceText = RequiredIsSpicy ? "Spicy " : "";
        Debug.Log($"New order received: Customer wants {spiceText}{RequiredBroth} Ramen with {RequiredNoodles}!");
    }


    public void CompleteOrder(Ingredient.IngredientName broth, Ingredient.IngredientName noodles, bool isSpicy)
    {
        if (HasActiveOrder)
        {
            CookedBroth = broth;
            CookedNoodles = noodles;
            CookedIsSpicy = isSpicy;
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

        bool brothMatch = CookedBroth == RequiredBroth;
        bool noodleMatch = CookedNoodles == RequiredNoodles;
        bool spiceMatch = CookedIsSpicy == RequiredIsSpicy;
        
        Debug.Log($" Time to complete order: {OrderTimer:F2} seconds."); 

        if (brothMatch && noodleMatch && spiceMatch)
        {
            string spiceText = RequiredIsSpicy ? "Spicy " : "";
            Debug.Log($"Correct order! {spiceText}{RequiredBroth} Ramen with {RequiredNoodles} served. Customer is happy!");
        }
        else
        {
            string expectedSpice = RequiredIsSpicy ? "Spicy " : "";
            string servedSpice = CookedIsSpicy ? "Spicy " : "";
            Debug.Log($"Wrong order! You served {servedSpice}{CookedBroth} Ramen with {CookedNoodles}, but they wanted {expectedSpice}{RequiredBroth} with {RequiredNoodles}. Customer is unhappy!");
        }

        // Stop the timer
        isTiming = false;

        // Reset order state
        HasActiveOrder = false;
        IsOrderCompleted = false;
        OrderTimer = 0f; // optional reset
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
