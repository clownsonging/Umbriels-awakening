using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    private Transform childHolder;
    [SerializeField] private GameObject North;
    [SerializeField] private GameObject South;
    [SerializeField] private GameObject East;
    [SerializeField] private GameObject West;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    public void Portals(bool north, bool east, bool south, bool west)
    {
        North.SetActive(north);
        South.SetActive(south);
        East.SetActive(east);
        West.SetActive(west);
    }
}
