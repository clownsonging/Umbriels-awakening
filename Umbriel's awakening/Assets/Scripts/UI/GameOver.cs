using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Canvas gm;
    [SerializeField] private PlayerStats stats;

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>(); 
    }
    public void RestartClick()
    {
        Time.timeScale = 1;
        stats.ResetStats();
        gm.enabled = false;
        SceneManager.LoadScene("FirstScene");  
    }

    public void Quit()
    {
        Time.timeScale = 1;
        stats.ResetStats();
        gm.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }
}

