using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    [SerializeField] private GameObject North;
    [SerializeField] private GameObject South;
    [SerializeField] private GameObject East;
    [SerializeField] private GameObject West;
    // Start is called before the first frame update
    public void Portals(bool north, bool east, bool south, bool west)
    {
        North.SetActive(north);
        South.SetActive(south);
        East.SetActive(east);
        West.SetActive(west);
    }
}
