using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        controls = player.movementControls;
        dashKey = player.dashButton;
        animation = player.animations;
        animation.ResetTrigger("Jump");
        animation.ResetTrigger("Idle");
        animation.SetTrigger("Walk");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Walk(player);
        if (IsMoving(player) == false)
        {
            player.SwitchState(player.IdleState);
        }
        if(dashKey.triggered)
        {
            player.SwitchState(player.DashState);
        }
    }
    public override void OnCollisionEnter(PlayerStateManager player)
    {

    }
    void Walk(PlayerStateManager player)
    {
        Vector2 v2 = controls.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(v2.x, 0f, v2.y).normalized;
        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;
        player.transform.position += (moveDirection) * player.GetComponent<PlayerStats>().Speed * Time.deltaTime;
    }
}
