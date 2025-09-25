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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(input.MoveInput * moveSpeed, rb.linearVelocity.y);
    }
}