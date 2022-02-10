using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerWalk WalkState = new PlayerWalk();
    public PlayerDash DashState = new PlayerDash();

    public Animator animations;

    public float dash = 0.25f;
    public InputAction movementControls;
    public InputAction dashButton;
    public Vector2 direction;
    public bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = WalkState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        direction = movementControls.ReadValue<Vector2>();
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    private void OnEnable()
    {
        movementControls.Enable();
        dashButton.Enable();
    }
    private void OnDisable()
    {
        movementControls.Disable();
        dashButton.Enable();
    }
}
