using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    public Image[] hearts;  // Array of hearts UI images
    public int lives = 3;   // Initial number of lives
    private int currentLives;

    void Start()
    {
        currentLives = lives;  // Initialize current lives
        UpdateHeartsUI();      // Update the hearts display on start
    }

    // This method is called when the player misses a vegetable
    public void MissVegetable()
    {
        if (currentLives > 0)
        {
            currentLives--;   // Decrease the lives by 1
            UpdateHeartsUI(); // Update the hearts display
             // Play the life lost sound
            FindObjectOfType<SoundEffectsManager>().PlayLifeLostSound();
        }

        if (currentLives <= 0)
        {
            // Transition to the main screen when lives run out
            GoToMainScreen();
        }
    }

    // Updates the hearts display based on remaining lives
    private void UpdateHeartsUI()
    {
        // Loop through all hearts and enable or disable based on current lives
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].enabled = true;  // Display the heart if lives remain
            }
            else
            {
                hearts[i].enabled = false; // Hide the heart if lives are lost
            }
        }
    }

    // Loads the main menu scene when the player runs out of lives
    private void GoToMainScreen()
    {
        SceneManager.LoadScene("EndingScene");  // Replace with your main menu scene name
    }
}
