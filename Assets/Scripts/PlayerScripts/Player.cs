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

        interaction.sceneUiManager = sceneUiManager;

        if (playerProfile == null)
        {
            Debug.LogWarning("PlayerProfile not assigned in Player script!");
            return;
        }

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerProfile.playerSprite != null && spriteRenderer.sprite != playerProfile.playerSprite)
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