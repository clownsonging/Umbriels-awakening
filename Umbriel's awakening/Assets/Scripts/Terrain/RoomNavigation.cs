using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private GameObject North;
    [SerializeField] private GameObject South;
    [SerializeField] private GameObject East;
    [SerializeField] private GameObject West;
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject spawn;
    private bool northBool;
    private bool southBool;
    private bool eastBool;
    private bool westBool;

    public GameObject Spawn { get => spawn; set => spawn = value; }

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
    
    public void SetPosition(int i, int j)
    {
        x = i;
        y = j;
    }
}
