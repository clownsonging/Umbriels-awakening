using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporters : MonoBehaviour
{
    [SerializeField] private int direction;
    [SerializeField] private GenScript map;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Generation2.0").GetComponent<GenScript>();
    }

    private void OnTriggerEnter()
    {
        map.NewRoom(direction);
    }
}   
