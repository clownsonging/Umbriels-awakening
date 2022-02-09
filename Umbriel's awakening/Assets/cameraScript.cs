using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    GameObject tracker;
    float scroll;
    // Start is called before the first frame update
    void Start()
    {
        scroll = 6;
        tracker = GameObject.Find("cameraTrack");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 setPosition = transform.position;
        setPosition.x = tracker.transform.position.x;
        setPosition.z = tracker.transform.position.z-1;
        setPosition.y = 5;
        transform.position = setPosition;
    }
    public void Scroll(float scroll)
    {
        scroll = scroll -1;
    }
}
