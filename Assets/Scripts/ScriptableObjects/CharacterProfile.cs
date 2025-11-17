using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Narrative/Character Profile")]
public class CharacterProfile : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public int baseRelationship = 0;
    
    public RamenRecipe signatureRamen; // their signature ramen
    public Ingredient.IngredientName preferredNoodles; // their noodle preference (Curly or Straight)
    public bool prefersSpicyMiso; // whether they prefer spicy miso (if they choose miso)

    [Header("Special Rules")]
    public bool acceptsAnyRamen;
}