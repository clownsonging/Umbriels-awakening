using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitDetection : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject ladder;

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
            Instantiate(ladder, this.transform.position, Quaternion.identity);
            Destroy(boss);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            player.GetComponent<PlayerStats>().DealDamage(contactDamage);
        }
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log("Boss Hp: " + health);
    }
}
