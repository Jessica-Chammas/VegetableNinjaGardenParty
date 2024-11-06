using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public void OnPlayAgainButtonClick()
    {
        // Play an optional sound effect when clicking "Play Again"
        SoundEffectsManager soundManager = FindObjectOfType<SoundEffectsManager>();
        if (soundManager != null)
        {
            soundManager.PlayStartGameSound(); // Assuming you want to reuse the start game sound
        }

        // Optionally delay the scene load to allow the sound to play
        StartCoroutine(LoadStartingSceneWithDelay());
    }

    private IEnumerator LoadStartingSceneWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Adjust delay to allow sound to play
        SceneManager.LoadScene("StartingScene"); // Replace with your main menu scene name
    }
}
