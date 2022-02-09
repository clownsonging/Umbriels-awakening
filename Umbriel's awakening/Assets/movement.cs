using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float jump = 3;
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -3;
    [SerializeField] private float rotationSpeed = 700;
    [SerializeField] private GameObject bullet;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float fireRate = 2;
    [SerializeField] private float nextAttack;
    [SerializeField] private bool cd;

    private float velocityY = 3;
    private CharacterController character;
    private GameObject _mainCamera;
    private bool Grounded;


    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    private void Start()
    {
        character = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Grounded = isGrounded();
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        Move(xMove, yMove);
        GravityCheck();
    }
    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.green);
        Ray ray = new Ray(this.transform.position + Vector3.up * -.5f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, .6f, ground))
            return true;
        else
            return false;
    }

    private void Move(float x, float y)
    {
        float xMove = x;
        float yMove = y;
        Vector3 moveDirection = new Vector3(xMove, 0.0f, yMove).normalized;
        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;
        transform.position += (moveDirection) * speed * Time.deltaTime + Vector3.up * velocityY;
        shoot(moveDirection);
    }

    private void shoot(Vector3 movement)
    {
        if (Input.GetKey("right") == true)
        {
            turn(145);
        }
        if (Input.GetKey("left") == true)
        {
            turn(-45);
        }
        if (Input.GetKey("down") == true)
        {
            turn(225);
        }
        if (Input.GetKey("up") == true)
        {
            turn(45);
        }
        if (Input.GetKey("right") == true && Input.GetKey("up") == true)
        {
            turn(90);
        }
        if (Input.GetKey("left") == true && Input.GetKey("up") == true)
        {
            turn(0);
        }
        if (Input.GetKey("left") == true && Input.GetKey("down") == true)
        {
            turn(270);
        }
        if (Input.GetKey("right") == true && Input.GetKey("down") == true)
        {
            turn(180);
        }
        if (Input.GetKey("right") == false && Input.GetKey("left") == false && Input.GetKey("up") == false && Input.GetKey("down") == false)
        {
            transform.forward = movement;
        }
    }
    public void GravityCheck()
    {
        if (Grounded == true)
        {
            Debug.Log("grounded");
            velocityY = 0;
        }
        else
        {
            Debug.Log("not grounded");
            velocityY = gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown("space") == true && isGrounded() == true)
        {
            Debug.Log("jumped");
            velocityY = 3;
        }
    }

    public void turn(float degree)
    {
        Quaternion toRotation = new Quaternion();
        toRotation = Quaternion.Euler(0, degree, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        fire();
    }
    public void fire()
    {
        if(cd == false)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            nextAttack = fireRate;
            cd = true;
        }
        else
        {
            nextAttack -= Time.deltaTime;
            if(nextAttack <= 0)
            {
                cd = false;
            }
        }
    }
}

