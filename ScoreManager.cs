using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the score UI text
    private int currentScore = 0; // Track the player's score

    void Start()
    {
        // Find the TextMeshProUGUI component for displaying the score
        if (scoreText == null)
        {
            GameObject scoreGO = GameObject.Find("Score");
            if (scoreGO != null)
            {
                scoreText = scoreGO.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogError("Score TextMeshProUGUI not found.");
            }
        }

        // Initialize the score display
        UpdateScoreText();
    }

    // Method to add points to the current score
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UpdateScoreText(); // Update the UI after adding points
    }

    // Method to update the score display text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
        else
        {
            Debug.LogError("Score TextMeshProUGUI reference is missing.");
        }
    }
}
