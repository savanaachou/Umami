using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [Header("Available Ramen Recipes")]
    public List<RamenRecipe> availableRecipes;

    [Header("Current Order")]
    public RamenRecipe activeRecipe;

    public bool HasActiveOrder { get; private set; }
    public bool IsOrderCompleted { get; private set; }

    public Ingredient.IngredientName RequiredBroth { get; private set; }
    public Ingredient.IngredientName RequiredNoodles { get; private set; }
    public bool RequiredIsSpicy { get; private set; }

    public Ingredient.IngredientName CookedBroth { get; private set; }
    public Ingredient.IngredientName CookedNoodles { get; private set; }
    public bool CookedIsSpicy { get; private set; }

    private CharacterProfile activeCharacter; // <-- NEW

    public float OrderTimer { get; private set; } = 0f;
    private bool isTiming = false;

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

    void Update()
    {
        if (isTiming && HasActiveOrder && !IsOrderCompleted)
        {
            OrderTimer += Time.deltaTime;
        }
    }

    // Take a random order
    public void TakeOrder()
    {
        if (availableRecipes == null || availableRecipes.Count == 0)
        {
            Debug.LogError("No ramen recipes assigned to OrderManager!");
            return;
        }

        activeRecipe = availableRecipes[Random.Range(0, availableRecipes.Count)];

        HasActiveOrder = true;
        IsOrderCompleted = false;

        RequiredBroth = activeRecipe.brothType;
        RequiredNoodles = GetRandomNoodles();
        RequiredIsSpicy = activeRecipe.canBeSpicy && Random.value > 0.5f;

        activeCharacter = null; // random orders have no character
        OrderTimer = 0f;
        isTiming = true;

        Debug.Log($"New order: {(RequiredIsSpicy ? "Spicy " : "")}{RequiredBroth} with {RequiredNoodles}");
    }

    // Take a story-driven order from a character
    public void TakeStoryOrder(CharacterProfile character)
    {
        HasActiveOrder = true;
        IsOrderCompleted = false;
        activeCharacter = character; // <-- assign activeCharacter
        activeRecipe = character.signatureRamen;

        OrderTimer = 0f;
        isTiming = true;

        if (character.acceptsAnyRamen)
        {
            RequiredBroth = Ingredient.IngredientName.None;
            RequiredNoodles = Ingredient.IngredientName.None;
            RequiredIsSpicy = false;

            Debug.Log($"{character.characterName} says: 'Surprise me! Cook me anything you like.'");
            return;
        }

        RequiredBroth = activeRecipe.brothType;
        RequiredNoodles = character.preferredNoodles;

        if (RequiredBroth == Ingredient.IngredientName.MisoBroth && activeRecipe.canBeSpicy)
            RequiredIsSpicy = character.prefersSpicyMiso;
        else
            RequiredIsSpicy = false;

        Debug.Log($"{character.characterName} ordered: {(RequiredIsSpicy ? "Spicy " : "")}{RequiredBroth} with {RequiredNoodles}");
    }

    // Complete order with what the player made
    public void CompleteOrder(Ingredient.IngredientName broth, Ingredient.IngredientName noodles, bool isSpicy)
    {
        if (HasActiveOrder)
        {
            CookedBroth = broth;
            CookedNoodles = noodles;
            CookedIsSpicy = isSpicy;
            IsOrderCompleted = true;

            Debug.Log("Ramen cooked! Ready to serve.");
        }
    }

    // Serve the order
    public void ServeOrder()
    {
        if (!HasActiveOrder)
        {
            Debug.Log("No order to serve!");
            return;
        }

        if (!IsOrderCompleted)
        {
            Debug.Log("Ramen isn’t ready yet!");
            return;
        }

        Debug.Log($"Time to complete order: {OrderTimer:F2} seconds.");

        // NPC That Accepts ANY Ramen
        if (activeCharacter != null && activeCharacter.acceptsAnyRamen)
        {
            RelationshipManager.Instance.AddRelationship(activeCharacter, 1);
            Debug.Log($"{activeCharacter.characterName} happily accepts whatever you made! Relationship increased by +1");
            ResetOrder();
            return;
        }

        // Normal Recipe Validation
        bool brothMatch = CookedBroth == RequiredBroth;
        bool noodleMatch = CookedNoodles == RequiredNoodles;
        bool spiceMatch = CookedIsSpicy == RequiredIsSpicy;

        if (brothMatch && noodleMatch && spiceMatch)
        {
            string spiceText = RequiredIsSpicy ? "Spicy " : "";
            Debug.Log($"Correct order! {spiceText}{RequiredBroth} with {RequiredNoodles} served. Customer happy!");
            
            if (activeCharacter != null)
            {
                RelationshipManager.Instance.AddRelationship(activeCharacter, 1);
                Debug.Log($"{activeCharacter.characterName} relationship increased by +1 from correct ramen!");
            }

        }
        else
        {
            string expectedSpice = RequiredIsSpicy ? "Spicy " : "";
            string servedSpice = CookedIsSpicy ? "Spicy " : "";

            Debug.Log(
                $"Wrong order! Served {servedSpice}{CookedBroth} with {CookedNoodles}, " +
                $"but expected {expectedSpice}{RequiredBroth} with {RequiredNoodles}. Customer unhappy!"
            );
        }

        ResetOrder();
    }

    public void ResetOrder()
    {
        HasActiveOrder = false;
        IsOrderCompleted = false;
        OrderTimer = 0f;
        activeRecipe = null;
        activeCharacter = null; 
    }

    public void MarkOrderIncomplete()
    {
        if (HasActiveOrder)
        {
            IsOrderCompleted = false;
            Debug.Log("Order marked incomplete. Finish it again to serve!");
        }
    }

    // Helpers
    private Ingredient.IngredientName GetRandomNoodles()
    {
        Ingredient.IngredientName[] noodles = {
            Ingredient.IngredientName.CurlyNoodles,
            Ingredient.IngredientName.StraightNoodles
        };
        return noodles[Random.Range(0, noodles.Length)];
    }
}
