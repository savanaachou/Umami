using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RamenRecipe", menuName = "Ramen/Recipe")]
public class RamenRecipe : ScriptableObject
{
    public string recipeName;
    [Header("Core Flavor Profile")]
    public Ingredient.IngredientName brothType;
    public Ingredient.IngredientName noodleType;
    
    [Header("Required Toppings")]
    public List<Ingredient.IngredientName> requiredToppings;

    [Header("Options")]
    public bool canBeSpicy;   // Only Miso should have this true

}

