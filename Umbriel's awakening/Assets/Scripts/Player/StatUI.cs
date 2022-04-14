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
    [SerializeField] private Text bombText;

    [Header("Advanced UI Containers")]
    [SerializeField] private Text speedText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text rangeText;
    [SerializeField] private Text attackSpeedText;
    [SerializeField] private Text enemiesText;

    [Header("Escape menu")]
    [SerializeField] private Canvas menu;

    [Header("Game over UI")]
    [SerializeField] private Canvas gameover;

    private bool AUI = false;


    private void Start()
    {
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
        if(stats.Hp <= 0)
        {
            Time.timeScale = 0;
            gameover.enabled = true;
        }
    }
    public void UpdateUI()
    {
        //Base UI
        hpText.text = "Health: " + stats.Hp + "/" + stats.CurrentHp;
        goldText.text = "Gold: " + stats.Gold;
        bombText.text = "Bombs: " + stats.Bombs;

        //Advanced UI
        enemiesText.text = "Enemies Left:" + stats.EnemiesLeft;
        speedText.text = "Speed: " + stats.Speed;
        damageText.text = "Damage: " + stats.Damage;
        rangeText.text = "Range: " + stats.Range;
        attackSpeedText.text = "Attack speed: " + stats.AttackSpeed;
    }

    void ToggleAdvancedUI()
    {
        if(AUI == false)
        {
            enemiesText.enabled = false;
            speedText.enabled = false;
            damageText.enabled = false;
            rangeText.enabled = false;
            attackSpeedText.enabled = false;
            AUI = true;
        }
        else
        {
            enemiesText.enabled = true;
            speedText.enabled = true;
            damageText.enabled = true;
            rangeText.enabled = true;
            attackSpeedText.enabled = true;
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
