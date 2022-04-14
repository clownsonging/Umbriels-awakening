using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void RestartClick()
    {
        Debug.Log("Starting game.");
        Time.timeScale = 1;
        SceneManager.LoadScene("FirstScene");
    }

    public void OptionsClick()
    {
        Debug.Log("Opening Options");
    }

    public void QuitClick()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
