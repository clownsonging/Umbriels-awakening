using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamushAttacks : MonoBehaviour
{
    [SerializeField] private Animator animations;
    [SerializeField] private GameObject player;
    
    [Header("Speed Variables")]
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float minSpeed = .5f;
    [SerializeField] private float speedCoefficient = 1f;
    [SerializeField] private Rigidbody rb;

    private Vector3 direction;
    private float lockPos = 0f;
    private bool isAttacking = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        MoveTo(player.transform.position);
        transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);
    }

    private void MoveTo(Vector3 x)
    {
        float distance = Vector3.Distance(x, transform.position);
        float speed = Mathf.Clamp(distance * speedCoefficient, minSpeed, maxSpeed);

        if(distance < 5)
        {
            StartCoroutine(Attack());
        }
        else
        {
            direction = (x - transform.position).normalized;
            rb.velocity = direction * speed;
            float singleStep = speedCoefficient * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
            transform.rotation = Quaternion.AngleAxis(180, direction);
        }
    }

    private IEnumerator Attack()
    {
        if(isAttacking)
        {
            yield break;
        }
        isAttacking = true;

        animations.SetTrigger("Attack");
        yield return new WaitForSeconds(animations.GetCurrentAnimatorStateInfo(0).length + animations.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animations.ResetTrigger("Attack");

        isAttacking = false;
    }
}
