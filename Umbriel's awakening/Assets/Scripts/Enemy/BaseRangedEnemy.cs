using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRangedEnemy : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 direction;
    private bool damageCooldown = false;
    private float damageTimer = .3f;
    private float lockPos = 0f;
    private bool attacking = false;

    [Header("Speed Variables")]
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float minSpeed = .5f;
    [SerializeField] private float speedCoefficient = 1f;
    [SerializeField] private Rigidbody rb;

    [Header("Targetting")]
    [SerializeField] private GameObject player;
    [SerializeField] private float sightRadius = 5;
    [SerializeField] private int attackRange = 3;
    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject attackSpawn;

    [Header("Stats")]
    [SerializeField] private float health = 10;
    [SerializeField] private int damage = 10;

    private void Awake()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > sightRadius && attacking == false)
        {
            moveTo(roamPosition);
        }
        else
        {
            roamPosition = this.transform.position;
            Vector3 targetDirection = player.transform.position - this.transform.position;
            float singleStep = speedCoefficient * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            Debug.Log("attacking player");
            if (damageCooldown == false)
            {
                Fire();
            }
            else
            {
                damageTimer = damageTimer - Time.deltaTime;
                if (damageTimer <= 0)
                {
                    damageCooldown = false;
                }
            }
        }
        if (health <= 0)
        {
            player.GetComponent<PlayerStats>().EnemiesLeft--;
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<BaseEnemyAI>().Bonk();
            Debug.Log("bonk");
        }
    }
    private void moveTo(Vector3 target)
    {
        direction = (target - transform.position).normalized;
        float distance = Vector3.Distance(target, transform.position);
        float speed = Mathf.Clamp(distance * speedCoefficient, minSpeed, maxSpeed);
        rb.velocity = direction * speed;
        if (Vector3.Distance(this.transform.position, roamPosition) < .1f)
        {
            GetRoamingPosition();
        }
    }
    private Vector3 GetRoamingPosition()
    {
        Vector3 newPosition = startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized * Random.Range(10f, 10f);
        roamPosition = newPosition;
        return newPosition;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }

    public void Bonk()
    {
        GetRoamingPosition();
    }
    public void Fire()
    {
        damageTimer = 1f;
        Instantiate(attack, attackSpawn.transform.position, this.transform.rotation);
        damageCooldown = true;
    }
}
