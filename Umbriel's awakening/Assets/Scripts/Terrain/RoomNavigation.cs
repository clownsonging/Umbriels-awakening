using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    [SerializeField] private GameObject North;
    [SerializeField] private GameObject South;
    [SerializeField] private GameObject East;
    [SerializeField] private GameObject West;
    [SerializeField] private int enemyCount;
    private bool northBool;
    private bool southBool;
    private bool eastBool;
    private bool westBool;

    // Start is called before the first frame update
    public void Portals(bool north, bool east, bool south, bool west)
    {
        northBool = north;
        southBool = south;
        eastBool = east;
        westBool = west;
    }

    public void ActivatePortals()
    {
        North.SetActive(northBool);
        South.SetActive(southBool);
        East.SetActive(eastBool);
        West.SetActive(westBool);
    }

    public int EnemyCount()
    {
        return enemyCount;
    }
}
