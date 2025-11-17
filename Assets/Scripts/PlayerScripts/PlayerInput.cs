using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerInput : MonoBehaviour
{
    public float MoveInput { get; private set; }
    public bool InteractPressed { get; private set; }

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movement input
        if (Input.GetKey(KeyCode.A))
            MoveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            MoveInput = 1f;
        else
            MoveInput = 0f;

        // Flip logic (invert from before)
        if (MoveInput < 0)
        {
            // Facing left: flipX = false (if right is the default)
            spriteRenderer.flipX = false;
        }
        else if (MoveInput > 0)
        {
            // Facing right: flipX = true
            spriteRenderer.flipX = true;
        }

        // Interaction input
        InteractPressed = Input.GetKeyDown(KeyCode.W);
    }
}