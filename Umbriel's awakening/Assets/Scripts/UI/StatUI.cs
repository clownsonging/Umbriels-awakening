using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StatUI : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private InputAction advancedUI;
    [SerializeField] private InputAction debugTP;
    [SerializeField] private InputAction esc;
    [SerializeField] private GameObject tpLocation;
    [SerializeField] private GameObject player;

    [Header("UI Containers")]
    [SerializeField] private Text hpText;
    [SerializeField] private Text goldText;

    [Header("Advanced UI Containers")]
    [SerializeField] private Text speedText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text rangeText;
    [SerializeField] private Text attackSpeedText;
    [SerializeField] private Text enemiesText;
    [SerializeField] private Canvas ACanvas;

    [Header("Escape menu")]
    [SerializeField] private Canvas menu;

    [Header("Game over UI")]
    [SerializeField] private Canvas gameover;

    private bool AUI = false;


    private void Start()
    {
        Time.timeScale = 1;
        gameover.enabled = false;
        menu.enabled = false;
        advancedUI.Enable();
        debugTP.Enable();
        esc.Enable();
        UpdateUI();
        ToggleAdvancedUI();
    }
    private void Update()
    {
        if (debugTP.triggered)
        {
            Debug.Log("Tp'ing");
            player.transform.position = tpLocation.transform.position;
        }
        if (advancedUI.triggered)
        {
            ToggleAdvancedUI();
        }
        if (esc.triggered)
        {
            Menu();
        }
        if(stats.CurrentHp <= 0)
        {
            Time.timeScale = 0;
            gameover.enabled = true;
        }
    }
    public void UpdateUI()
    {
        //Base UI
        hpText.text = stats.CurrentHp + "/" + stats.Hp;
        goldText.text = stats.Gold.ToString();

        //Advanced UI
        enemiesText.text = stats.EnemiesLeft.ToString();
        speedText.text = stats.Speed.ToString();
        damageText.text = stats.Damage.ToString();
        rangeText.text = stats.Range.ToString();
        attackSpeedText.text = stats.AttackSpeed.ToString();
    
        if(stats.EnemiesLeft == -1)
        {
            enemiesText.text = "0";
        }
    }

    void ToggleAdvancedUI()
    {
        if(AUI == false)
        {
            ACanvas.enabled = false;
            AUI = true;
        }
        else
        {
            ACanvas.enabled = true;
            AUI = false;
        }
    }
    void Menu()
    {
        if(Time.timeScale == 1)
        {
            Debug.Log("Freezing");
            menu.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Unfreezing");
            menu.enabled = false;
            Time.timeScale = 1;
        }
    }
}
