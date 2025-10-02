using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Your character
    public float smoothSpeed = 0.125f; 
    public Vector3 offset;     // Optional offset from the player

    // Camera limits
    public float minX; // Left boundary
    public float maxX; // Right boundary

    void LateUpdate()
    {
        if (player == null) return;

        // Follow the player on X only
        float targetX = player.position.x + offset.x;

        // Clamp camera position so it doesn’t move past the borders
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        Vector3 desiredPosition = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}