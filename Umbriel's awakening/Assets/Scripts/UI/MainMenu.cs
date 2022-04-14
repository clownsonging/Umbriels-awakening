using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Here");
    }
    public void StartClick()
    {
        Debug.Log("Starting game.");
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

