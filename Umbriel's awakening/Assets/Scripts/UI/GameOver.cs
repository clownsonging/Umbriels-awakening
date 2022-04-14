using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartClick()
    {
        Debug.Log("Starting game.");
        Time.timeScale = 1;
        SceneManager.LoadScene("FirstScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}

