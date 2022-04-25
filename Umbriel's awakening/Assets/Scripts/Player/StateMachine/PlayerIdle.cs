using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerIdle : PlayerBaseState
{ 
    public override void EnterState(PlayerStateManager player)
    {
        controls = player.movementControls;
        animation = player.animations;
        animation.ResetTrigger("Jump");
        animation.ResetTrigger("Walk");
        animation.SetTrigger("Idle");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (IsMoving(player) == true)
        {
            player.SwitchState(player.WalkState);
        }
    }
    public override void OnCollisionEnter(PlayerStateManager player)
    {

    }
}
