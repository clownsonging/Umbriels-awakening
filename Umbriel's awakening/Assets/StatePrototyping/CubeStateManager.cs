using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStateManager : MonoBehaviour
{

    CubeBaseState currentState;
    public CubeAttackState AttackState = new CubeAttackState();
    public CubeDashState DashState = new CubeDashState();
    public CubeIdleState IdleState = new CubeIdleState();
    public CubeMoveState MoveState = new CubeMoveState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(CubeBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
