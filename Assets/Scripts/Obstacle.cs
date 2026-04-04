using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Size Settings")]
    readonly float minSize = 0.5f, maxSize = 1.5f;

    [Header("Movement Settings")]
    readonly float minSpeed = 50f, maxSpeed = 180f;
    readonly float spinSpeed = 10f;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        DifficultyManager.OnDifficultyChanged += ApplySpeedMultiplier;
    }

    private void OnDisable()
    {
        DifficultyManager.OnDifficultyChanged -= ApplySpeedMultiplier;
    }

    void Start()
    {
        InitializeObstacle();
    }

    void InitializeObstacle()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        rigidbody2D.AddForce(Random.insideUnitCircle * randomSpeed);
        rigidbody2D.AddTorque(Random.Range(-spinSpeed, spinSpeed));
    }

    private void ApplySpeedMultiplier(float multiplier)
    {
        rigidbody2D.linearVelocity *= multiplier;
    }
}