using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporters : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private Generation map;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Generation").GetComponent<Generation>();
    }

    private void OnTriggerEnter()
    {
        Debug.Log("Hit: " + direction);
        map.NewRoom(direction);
    }
}   
