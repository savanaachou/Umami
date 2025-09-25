using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerInteraction))]
public class Player : MonoBehaviour
{
    public UIManager uiManager;

    private PlayerInput input;
    private PlayerMovement movement;
    private PlayerInteraction interaction;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<PlayerInteraction>();

        // Pass references down if needed
        interaction.uiManager = uiManager;
    }
}