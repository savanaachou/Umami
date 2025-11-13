using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "Narrative/Character Profile")]
public class CharacterProfile : ScriptableObject
{
    public string characterName;
    [Range(0, 100)] public int relationship;
    [TextArea] public string description;
}