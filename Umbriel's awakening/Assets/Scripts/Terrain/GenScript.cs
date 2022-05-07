using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenScript : MonoBehaviour
{
    [Header("Floor inputs")]
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private GameObject[] roomList;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject itemRoom;
    [SerializeField] private GameObject shopRoom;


    [Header("Player settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private int playerX;
    [SerializeField] private int playerY;

    private int heightValue;
    private int widthValue;
    private GameObject[,] floorGrid;
    private GameObject currentRoom;
    private MapUI map;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject nullRoom;
    [SerializeField] private GameObject roomStore;

    public int Height { get => height; set => height = value; }
    public int Width { get => width; set => width = value; }
    public GameObject[,] FloorGrid { get => floorGrid; set => floorGrid = value; }
    public int PlayerX { get => playerX; set => playerX = value; }
    public int PlayerY { get => playerY; set => playerY = value; }




    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(player);
            Debug.Log("Spawning player");
        }
        player = GameObject.FindGameObjectWithTag("Player");
        GenerateBaseFloor();
        map = GameObject.FindGameObjectWithTag("MapUI").GetComponent<MapUI>();
    }

    private void GenerateBaseFloor()
    {
        //Roll dimensions
        heightValue = Random.Range(8, height);
        widthValue = Random.Range(8, width);
        height = heightValue;
        width = widthValue;

        //Setup grid & fill with null rooms
        floorGrid = new GameObject[widthValue, heightValue];
        int roomCount = heightValue + widthValue;
        Debug.Log("Grid is " + widthValue + " by " + heightValue + ".");
        fillFloor(nullRoom);

        //Create spawn + normal rooms + boss/item/shops and deactivate nulls
        SpawnRoom(Mathf.RoundToInt(widthValue / 2), Mathf.RoundToInt(heightValue / 2), spawn);
        SpawnNormalRoom(roomCount);
        SpawnSpecialRooms();
        DeactivateNulls();

        //Set player in spawn
        player.transform.position = new Vector3(floorGrid[Mathf.RoundToInt(widthValue / 2), Mathf.RoundToInt(heightValue / 2)].transform.position.x, 1, floorGrid[Mathf.RoundToInt(widthValue / 2), Mathf.RoundToInt(heightValue / 2)].transform.position.z);
        playerX = Mathf.RoundToInt(widthValue / 2);
        playerY = Mathf.RoundToInt(heightValue / 2);
        NewRoom(0);
        RoomClear();
        
    }

    private void fillFloor(GameObject room)
    {
        for (int i = 0; i < widthValue; i++)
        {
            for (int j = 0; j < heightValue; j++)
            {
                floorGrid[i,j] = Instantiate(room, new Vector3((i * 20) + i, 0, (j * 20) + j), Quaternion.identity, roomStore.transform);
            }
        }
    }

    private bool ValidSpawn(int x, int y)
    {
        if(floorGrid[x,y].gameObject.tag == "NullRoom" && CheckAdjacents(x, y) == true)
        { 
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckAdjacents(int x, int y)
    {
        bool roomAdjacent = false;
        try
        {
            if (floorGrid[x + 1, y].gameObject.tag == "Room")
            {
                roomAdjacent = true;
            }
            if (floorGrid[x - 1, y].gameObject.tag == "Room")
            {
                roomAdjacent = true;
            }
            if (floorGrid[x, y - 1].gameObject.tag == "Room")
            {
                roomAdjacent = true;
            }
            if (floorGrid[x, y + 1].gameObject.tag == "Room")
            {
                roomAdjacent = true;
            }
        }
        catch
        {
            return roomAdjacent;
        }
        return roomAdjacent;
    }

    private void SpawnRoom(int x, int y, GameObject room)
    {
        Destroy(floorGrid[x, y]);
        floorGrid[x, y] = Instantiate(room, new Vector3(x * 50, 0, y * 50), Quaternion.identity, roomStore.transform);
        floorGrid[x, y].GetComponentInChildren<RoomNavigation>().SetPosition(x, y);
    }

    private bool SpecialRoomCheck(int x, int y)
    {
        try
        {
            int valid = 0;
            if (floorGrid[x + 1, y].gameObject.tag == "Room")
            {
                valid++;
            }
            if (floorGrid[x - 1, y].gameObject.tag == "Room")
            {
                valid++;
            }
            if (floorGrid[x, y + 1].gameObject.tag == "Room")
            {
                valid++;
            }
            if (floorGrid[x, y - 1].gameObject.tag == "Room")
            {
                valid++;
            }
            if (floorGrid[x, y].gameObject.tag == "Room")
            {
                valid = 10;
            }
            if (valid == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    private void DeactivateNulls()
    {
        for (int i = 0; i < widthValue; i++)
        {
            for (int j = 0; j < heightValue; j++)
            {
                if(floorGrid[i,j].gameObject.tag == "NullRoom")
                {
                    floorGrid[i,j].SetActive(false);
                }
            }
        }
    }

    private void SpawnNormalRoom(int amount)
    {
        while (amount > 0)
        {
            currentRoom = roomList[Random.Range(0, roomList.Length)];
            int x = Random.Range(0, widthValue);
            int y = Random.Range(0, heightValue);

            if (ValidSpawn(x, y) == true)
            {
                SpawnRoom(x, y, currentRoom);
                floorGrid[x, y].SetActive(false);
                amount--;
            }
        }
    }

    private void SpawnSpecialRooms()
    {
        bool shop = false;
        bool boss = false;
        bool item = false;
        while(shop == false)
        {
            int x = Random.Range(0, widthValue);
            int y = Random.Range(0, heightValue);
            if(SpecialRoomCheck(x,y) == true)
            {
                SpawnRoom(x, y, shopRoom);
                floorGrid[x, y].SetActive(false);
                shop = true;
            }
        }

        while (boss == false)
        {
            int x = Random.Range(0, widthValue);
            int y = Random.Range(0, heightValue);
            if (SpecialRoomCheck(x, y) == true)
            {
                SpawnRoom(x, y, bossRoom);
                floorGrid[x, y].SetActive(false);
                boss = true;
            }
        }

        while (item == false)
        {
            int x = Random.Range(0, widthValue);
            int y = Random.Range(0, heightValue);
            if (SpecialRoomCheck(x, y) == true)
            {
                SpawnRoom(x, y, itemRoom);
                floorGrid[x, y].SetActive(false);
                item = true;
            }
        }
    }

    public void RoomClear()
    {
        floorGrid[playerX, playerY].GetComponentInChildren<RoomNavigation>().ActivatePortals();
        int coinAmount = Random.Range(1, 5);
        while (coinAmount > 0)
        {
            Instantiate(coin, new Vector3(floorGrid[playerX, playerY].transform.position.x + Random.Range(0, 3), floorGrid[playerX, playerY].transform.position.y+1f, floorGrid[playerX, playerY].transform.position.z + Random.Range(0, 3)), Quaternion.identity);
            coinAmount--;
        }
    }

    public void NewRoom(int direction)
    {
        bool north = false;
        bool east = false;
        bool south = false;
        bool west = false;
        //North
        if (direction == 1)
        {
            playerY++;
        }
        //East
        if (direction == 2)
        {
            playerX++;
        }
        //South
        if (direction == 3)
        {
            playerY--;
        }
        //West
        if (direction == 4)
        {
            playerX--;
        }
        player.transform.position = floorGrid[playerX, playerY].GetComponentInChildren<RoomNavigation>().Spawn.transform.position; 
        floorGrid[playerX, playerY].SetActive(true);

        if (floorGrid[playerX + 1, playerY].gameObject.tag != "NullRoom")
        {
            east = true;
        }
        if (floorGrid[playerX, playerY - 1].gameObject.tag != "NullRoom")
        {
            south = true;
        }
        if (floorGrid[playerX - 1, playerY].gameObject.tag != "NullRoom")
        {
            west = true;
        }
        if (floorGrid[playerX, playerY + 1].gameObject.tag != "NullRoom" )
        {
            north = true;
        }
        TeleportersSet(playerX, playerY, north, east, south, west);
        player.GetComponent<PlayerStats>().NewRoom(floorGrid[playerX, playerY].GetComponentInChildren<RoomNavigation>());
    }

    void TeleportersSet(int x, int y, bool north, bool east, bool south, bool west)
    {
        floorGrid[x, y].GetComponentInChildren<RoomNavigation>().Portals(north, east, south, west);
    }

    public GameObject[,] GetMap()
    {
        return floorGrid;
    }
}
