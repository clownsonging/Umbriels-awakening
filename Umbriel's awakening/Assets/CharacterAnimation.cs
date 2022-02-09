using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Trigger();
    }
    private void Trigger()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetTrigger("Walk");
        }
    }
}
