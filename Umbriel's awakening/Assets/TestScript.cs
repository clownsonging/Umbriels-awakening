using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    public InputAction controls;
    // Start is called before the first frame update
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        Vector2 v2 = controls.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(v2.x, 0f, v2.y).normalized;
        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;
        transform.position += (moveDirection) * 10 * Time.deltaTime;
    }
    // Update is called once per frame
    void test()
    {
        Debug.Log("hit test script test method");
    }
}
