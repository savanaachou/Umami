using System.Collections.Generic;
using UnityEngine;

public class RelationshipManager : MonoBehaviour
{
    public static RelationshipManager Instance { get; private set; }

    private Dictionary<string, int> relationshipValues = new Dictionary<string, int>();

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

    public int GetRelationship(CharacterProfile character)
    {
        string name = character.characterName;

        if (!relationshipValues.ContainsKey(name))
            relationshipValues[name] = character.baseRelationship;

        return relationshipValues[name];
    }

    public void AddRelationship(CharacterProfile character, int amount)
    {
        string name = character.characterName;

        if (!relationshipValues.ContainsKey(name))
            relationshipValues[name] = character.baseRelationship;

        relationshipValues[name] += amount;

        Debug.Log($"{name} relationship now {relationshipValues[name]}");
    }

}