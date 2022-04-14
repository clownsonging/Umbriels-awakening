using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private StatUI UI;
    
    private void Start()
    {
        UI = GameObject.FindGameObjectWithTag("Player").GetComponent<StatUI>();
    }
    public int Hit()
    {
        Destroy(this.gameObject);
        UI.UpdateUI();
        return value;
    }
}