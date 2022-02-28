using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    private GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        item = GameObject.FindGameObjectWithTag(this.name);
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        Debug.Log("hit item: " + this.name);
        item.SetActive(true);
    }
}
