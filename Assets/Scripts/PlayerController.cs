using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public GameObject boosterFlame;
    public UIDocument uiDocument;
    public GameObject explosionEffect;
    public GameObject backGroundEffect;

    Rigidbody2D rb;
    Vector2 direction;
    Label scoreText;
    Button restartButton;
    ParticleSystem bgParticle;
    float elapsedTime = 0;
    float score = 0f;
    float scoreMultiplier = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        GameObject bgInstance = Instantiate(backGroundEffect);
        bgParticle = bgInstance.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        UpdateScore();
        MovePlayer();
        BoosterFlameMechanic();
    }

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score : " + score;
    }

    void MovePlayer()
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
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
    
        if(bgParticle != null)
            bgParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
