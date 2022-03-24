using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerStats playerStats;

    private float attackCd = 0;
    // Update is called once per frame
    void Update()
    {
    }
    public void Fire()
    {
        if (attackCd <= 0)
        {
            attackCd = playerStats.AttackSpeed;
            Instantiate(attack, player.transform.position, player.transform.rotation);
        }
        else
        {
            attackCd = attackCd - Time.deltaTime;
        }
    }
}
