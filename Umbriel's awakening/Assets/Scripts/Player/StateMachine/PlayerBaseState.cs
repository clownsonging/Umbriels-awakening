using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public abstract class PlayerBaseState
{
    public InputAction controls;
    public InputAction dashKey;
    public Animator animation;
    public abstract void EnterState(PlayerStateManager player);

    public abstract void UpdateState(PlayerStateManager player);

    public abstract void OnCollisionEnter(PlayerStateManager player);

    public bool IsMoving(PlayerStateManager player)
    {
        Vector2 v2 = controls.ReadValue<Vector2>();
        if (v2.x != 0 || v2.y != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void test()
    {
        Debug.Log("hit test at : " + this);
    }
}
