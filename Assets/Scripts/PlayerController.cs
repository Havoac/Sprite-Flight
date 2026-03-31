using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public GameObject boosterFlame;
    Rigidbody2D rb;
    Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            // Calculate Mouse Direction
            Vector3 mouseClickedPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            direction = (mouseClickedPosition - transform.position).normalized;
        }
        else
        {
            if (Keyboard.current.upArrowKey.isPressed)
                direction += Vector2.up;

            if (Keyboard.current.downArrowKey.isPressed)
                direction += Vector2.down;

            if (Keyboard.current.leftArrowKey.isPressed)
                direction += Vector2.left;

            if (Keyboard.current.rightArrowKey.isPressed)
                direction += Vector2.right;

            direction = direction.normalized;
        }

        // Move Player in direction to the mouse
        transform.up = direction;
        rb.AddForce(direction * thrustForce);

        BoosterFlameMechanic();
    }

    void BoosterFlameMechanic()
    {
        // Mouse Mechanic
        if (Mouse.current.leftButton.wasPressedThisFrame)
            boosterFlame.SetActive(true);
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
            boosterFlame.SetActive(false);

        // Keyboard Mechanic
        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            boosterFlame.SetActive(true);
        else if(Keyboard.current.upArrowKey.wasReleasedThisFrame || Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current.leftArrowKey.wasReleasedThisFrame || Keyboard.current.rightArrowKey.wasReleasedThisFrame)
            boosterFlame.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
