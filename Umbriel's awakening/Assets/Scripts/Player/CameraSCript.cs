using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x - .5f, 8, player.transform.position.z - .5f);

    }
}
