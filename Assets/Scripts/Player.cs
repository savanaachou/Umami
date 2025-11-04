using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerInteraction))]
public class Player : MonoBehaviour
{
    public SceneUIManager sceneUiManager;
    public PlayerProfile playerProfile;   // Assign your ScriptableObject here
    public SpriteRenderer spriteRenderer; // The sprite renderer on the player object

    private PlayerInput input;
    private PlayerMovement movement;
    private PlayerInteraction interaction;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<PlayerInteraction>();

        // Pass references down if needed
        interaction.sceneUiManager = sceneUiManager;

        // Set the player sprite from the profile
        if (playerProfile != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = playerProfile.playerSprite;
            Debug.Log($"Player sprite set to: {spriteRenderer.sprite.name}");
        }
    }
    
    public void UpdatePlayerSprite()
    {
        if (playerProfile != null && spriteRenderer != null && playerProfile.playerSprite != null)
        {
            spriteRenderer.sprite = playerProfile.playerSprite;
            Debug.Log($"Player sprite updated to: {spriteRenderer.sprite.name}");
        }
    }
    
}