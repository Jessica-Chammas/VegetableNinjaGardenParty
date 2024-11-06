using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
       public void OnPlayAgainButtonClick()
    {
        // Load the main gameplay scene (replace "MainScene" with the actual name of your gameplay scene)
        SceneManager.LoadScene("StartingScene");
    }
}
