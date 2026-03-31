using UnityEngine;

public class Obstacle : MonoBehaviour
{
    readonly float minSize = 0.5f, maxSize = 1.5f;
    readonly float minSpeed = 50f, maxSpeed = 180f;
    readonly float spinSpeed = 10f;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Debug.Log("Obstacle created: " + gameObject.name);

        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        float randomSpeed = Random.Range(minSpeed, maxSpeed)/randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;

        rigidbody2D.AddForce(randomDirection * randomSpeed);

        float randomSpin = Random.Range(-spinSpeed, spinSpeed);
        rigidbody2D.AddTorque(randomSpin);
    }

    void Update()
    {

    }
}
