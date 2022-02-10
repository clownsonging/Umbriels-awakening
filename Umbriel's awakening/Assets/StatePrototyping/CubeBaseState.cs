using UnityEngine;

public abstract class CubeBaseState
{
    public abstract void EnterState(CubeStateManager cube);

    public abstract void UpdateState(CubeStateManager cube);

    public abstract void OnCollisionEnter(CubeStateManager cube);

}
