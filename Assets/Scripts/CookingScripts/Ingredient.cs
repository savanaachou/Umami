using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum IngredientType
    {
        Noodles,
        Broth,
        Topping
    }

    public enum IngredientName
    {
        None,
        // --- Noodles ---
        CurlyNoodles,
        StraightNoodles,

        // --- Broths ---
        TonkotsuBroth,
        ShoyuBroth,
        MisoBroth,

        // --- Toppings ---
        Chashu,
        Kakuni,
        Corn,
        GreenOnion,
        BeanSprout,
        Nori,
        BambooShoots,
        WoodEarMushrooms,
        Egg,
        ChiliOil
    }

    public IngredientType type;
    public IngredientName nameID;  // replaces ingredientName string
}