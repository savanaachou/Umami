using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private PlayerInput input;
    private Camera mainCam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        mainCam = Camera.main;

        if (PlayerData.HasSavedPosition)
        {
            transform.position = PlayerData.LastPosition;
        }
    }

    void FixedUpdate()
    {
        // Determine if player input should be active
        SceneUIManager uiManager = Object.FindFirstObjectByType<SceneUIManager>();
        bool isActive = !(PauseManager.Instance != null && PauseManager.Instance.IsPaused)
                        && !(uiManager?.IsOnStartScreen ?? false)
                        && !(uiManager?.IsOnPlayerSetupScreen ?? false);

        if (!isActive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(input.MoveInput * moveSpeed, rb.linearVelocity.y);
    }

    void LateUpdate()
    {
        // Keep player within camera view horizontally
        Vector3 pos = transform.position;

        // Convert to viewport space (0–1 across the screen)
        Vector3 viewPos = mainCam.WorldToViewportPoint(pos);

        // Clamp to slightly inside edges (0 = left, 1 = right)
        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);

        // Convert back to world space
        transform.position = mainCam.ViewportToWorldPoint(viewPos);
    }
}