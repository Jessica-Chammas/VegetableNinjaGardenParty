using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        // Play the start game sound
        SoundEffectsManager soundManager = FindObjectOfType<SoundEffectsManager>();
        if (soundManager != null)
        {
            soundManager.PlayStartGameSound();
        }

        // Optionally delay the scene load to allow the sound to play
        StartCoroutine(LoadMainSceneWithDelay());
    }

    private IEnumerator LoadMainSceneWithDelay()
    {
        yield return new WaitForSeconds(0.2f); // Adjust delay as needed to allow the sound to play
        SceneManager.LoadScene("MainScene"); // Replace with the actual scene name
    }
}
