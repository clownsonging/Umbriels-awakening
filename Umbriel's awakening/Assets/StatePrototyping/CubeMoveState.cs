using UnityEngine;

public class CubeMoveState : CubeBaseState 
{
    public override void EnterState(CubeStateManager cube)
    {
        Debug.Log("Entered idle state");
    }

    public override void UpdateState(CubeStateManager cube)
    {
        
    }

    public override void OnCollisionEnter(CubeStateManager cube)
    {

    }
}
