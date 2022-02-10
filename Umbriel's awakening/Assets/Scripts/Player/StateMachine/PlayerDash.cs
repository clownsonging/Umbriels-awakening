using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Dash state.");
        controls = player.movementControls;
        animation = player.animations;
        animation.ResetTrigger("Walk");
        animation.ResetTrigger("Idle");
        animation.SetTrigger("Jump");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        float startTime = player.dash;
        while (startTime >= 0)
        {
            Vector2 v2 = controls.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(v2.x, 0f, v2.y).normalized;
            moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;
            player.transform.position += (moveDirection) *2* Time.deltaTime;

            startTime -= Time.deltaTime;
        }
        player.SwitchState(player.WalkState);
    }
    public override void OnCollisionEnter(PlayerStateManager player)
    {

    }
}
