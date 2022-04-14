using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitDetection : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("Boss stats:")]
    [SerializeField] private int health;
    [SerializeField] private int contactDamage;
    [SerializeField] private int attackDamage;
    [SerializeField] private float speed;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            player.GetComponent<PlayerStats>().DealDamage(contactDamage);
        }
        if(collision.gameObject.tag == "PlayerAttack")
        {
            Debug.Log("hit by player");
        }
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
    }
}
