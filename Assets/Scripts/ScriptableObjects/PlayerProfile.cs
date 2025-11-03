using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfile", menuName = "Game/Player Profile")]
public class PlayerProfile : ScriptableObject
{
    [Header("Player Info")]
    public string playerName = "Player";  // default name
    public Sprite playerSprite;           // chosen character sprite

    //[Header("Optional Data")]
    //public int level = 1;
    //public int coins = 0;

    // You can add more persistent fields here like stats, unlocked items, etc.
}