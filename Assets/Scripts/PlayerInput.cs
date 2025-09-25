using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveInput { get; private set; }
    public bool InteractPressed { get; private set; }

    void Update()
    {
        // Movement input
        if (Input.GetKey(KeyCode.A))
            MoveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            MoveInput = 1f;
        else
            MoveInput = 0f;

        // Interaction input
        InteractPressed = Input.GetKeyDown(KeyCode.W);
    }
}