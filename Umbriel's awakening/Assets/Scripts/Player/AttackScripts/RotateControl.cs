using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateControl : MonoBehaviour
{
    [SerializeField] private AttackScript attack;
    public InputAction arrows;
    public Vector3 player;
    float angle;
    // Update is called once per frame
    void Update()
    {
        Quaternion toRotation = new Quaternion();
        toRotation = Quaternion.Euler(0, Direction(), 0);
        transform.rotation = toRotation;
    }

    private void OnEnable()
    {
        arrows.Enable();
    }

    private void OnDisable()
    {
        arrows.Disable();
    }

    private float Direction()
    {
        //Debug.Log(arrows.ReadValue<Vector2>());
        Vector2 v2 = arrows.ReadValue<Vector2>();
        if (v2.x == 1)
        {
            angle = 135;
            attack.Fire();
            return angle;
        }
        if (v2.x == -1)
        {
            angle = 315;
            attack.Fire();
            return angle;
        }
        if (v2.y == 1)
        {
            angle =45;
            attack.Fire();
            return angle;
        }
        if (v2.y == -1)
        {
            angle = 225;
            attack.Fire();
            return angle;
        }
        if (v2.x > .1 && v2.y > .1)
        {
            angle = 90;
            attack.Fire();
            return angle;
        }
        if (v2.x < -.1 && v2.y < -.1)
        {
            angle = 270;
            attack.Fire();
            return angle;
        }
        if (v2.x > .1 && v2.y < -.1)
        {
            angle = 180;
            attack.Fire();
            return angle;
        }
        if (v2.x < -.1 && v2.y > .1)
        {
            angle = 0;
            attack.Fire();
            return angle;
        }
        else
        {
            return angle;
        }
    }
}
