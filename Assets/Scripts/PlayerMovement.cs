using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private PlayerInput input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();

        if (PlayerData.HasSavedPosition)
        {
            transform.position = PlayerData.LastPosition;
        }
    }


    void FixedUpdate()
    {
        // Determine if player input should be active
        bool isActive = !(PauseManager.Instance != null && PauseManager.Instance.IsPaused)
                        && !(FindObjectOfType<SceneUIManager>()?.IsOnStartScreen ?? false);

        if (!isActive)
        {
            // Stop movement completely
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(input.MoveInput * moveSpeed, rb.linearVelocity.y);
    }
}