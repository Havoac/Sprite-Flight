using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;
    public static event System.Action<float> OnDifficultyChanged;

    private int currentLevel = 1, maxLevel = 10;
    private int scoreOffset = 70;
    private readonly float speedMultiplierStep = 1.5f; // each level multiplies by this

    private void Awake()
    {
        Instance = this;
    }

    public void UpdatedDifficulty(float score)
    {
        int newLevel = Mathf.FloorToInt(score / scoreOffset) + 1;
        newLevel = Mathf.Clamp(newLevel, 1, maxLevel);

        if (newLevel != currentLevel)
        {
            currentLevel = newLevel;
            OnDifficultyChanged?.Invoke(speedMultiplierStep);
        }
    }

    public int GetDifficultyLevel()
    {
        return currentLevel;
    }
}