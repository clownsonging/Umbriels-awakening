using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject[] roomList;
    [SerializeField] private GameObject player;
    [SerializeField] private int playersRoomX;
    [SerializeField] private int playersRoomY;


    [SerializeField] private GameObject itemRoom;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject shopRoom;
    [SerializeField] private GameObject[,] roomGrid;
    [SerializeField] private GameObject roomStore;

    private GameObject room;

    private bool tileEmpty = false;
    private int height;
    private int width;
    private int noRooms;
    private int roomType;
    private bool validSpawn = false;

    public GameObject[,] RoomGrid { get => roomGrid; set => roomGrid = value; }

    // Start is called before the first frame update
    void Start()
    {
        GenerateFloor();
        NewRoom(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool[] CheckNeighbour(int x, int y)
    {
        bool[] answer = new bool[4];
        //north
        if (RoomGrid[x, y + 1] != null)
        {
            answer[0] = true;
        }
        else
        {
            answer[0] = false;
        }

        //east
        if (RoomGrid[x + 1, y] != null)
        {
            answer[1] = true;

        }
        else
        {
            answer[1] = false;
        }

        //south
        if (RoomGrid[x, y - 1] != null)
        {
            answer[2] = true;

        }
        else
        {
            answer[2] = false;
        }

        //west
        if (RoomGrid[x - 1, y] != null)
        {
            answer[3] = true;

        }
        else
        {
            answer[3] = false;
        }

        return answer;
    }
    void GenerateFloor()
    {
        //sets up counters and such
        height = Random.Range(6, 50);
        width = Random.Range(6, 50);
        noRooms = height + width;
        RoomGrid = new GameObject[width, height];

        //Sets first room type
        roomType = Random.Range(0, roomList.Length);
        room = roomList[roomType];

        //spawns starting room
        RoomGrid[Mathf.RoundToInt(width / 2), Mathf.RoundToInt(height / 2)] = room;
        GameObject spawn = Instantiate(room, new Vector3(Mathf.RoundToInt(width / 2) * 20, 0, Mathf.RoundToInt(height / 2) * 20), Quaternion.identity);
        Destroy(spawn);
        noRooms--;
        Debug.Log("Spawned first room at " + width + " width & " + height + " height.");

        //loops adding the rooms
        while (noRooms >= 0)
        {
            Debug.Log("Hit spawning loops");
            validSpawn = false;
            int i = Random.Range(1, height - 1);
            int j = Random.Range(1, width - 1);

            if (RoomGrid[j, i] != room)
            {
                tileEmpty = true;
            }

            if (RoomGrid[j + 1, i] != null && tileEmpty == true)
            {
                validSpawn = true;

            }

            if (RoomGrid[j - 1, i] != null && tileEmpty == true)
            {
                validSpawn = true;

            }

            if (RoomGrid[j, i + 1] != null && tileEmpty == true)
            {
                validSpawn = true;

            }

            if (RoomGrid[j, i - 1] != null && tileEmpty == true)
            {
                validSpawn = true;

            }

            //spawns room
            if (validSpawn == true)
            {
                roomType = Random.Range(0, roomList.Length);
                room = roomList[roomType];
                RoomGrid[j, i] = Instantiate(room, new Vector3((j * 20) + j, 0, (i * 20) + i), Quaternion.identity, roomStore.transform);
                noRooms--;
               // Debug.Log("Spawned room at " + j + " width & " + i + " height.");
                playersRoomX = j;
                playersRoomY = i;
                RoomGrid[j, i].SetActive(false);
            }
        }
        roomGrid[playersRoomX, playersRoomY].SetActive(true);
        player.transform.position = roomGrid[playersRoomX, playersRoomY].transform.position;
        SpecialSpawn(itemRoom);
        SpecialSpawn(bossRoom);
        SpecialSpawn(shopRoom);
        Debug.Log("Finished rooms");
    }
    void TeleportersSet(int x, int y, bool north, bool east, bool south, bool west)
    {
        roomGrid[x,y].GetComponent<RoomNavigation>().Portals(north, east, south, west);
    }

    public void NewRoom(int direction)
    {
        bool north = false;
        bool east = false;
        bool south = false;
        bool west = false;
        //North
        if(direction == 1)
        {
            playersRoomY++;
        }
        //East
        if (direction == 2)
        {
            playersRoomX++;
        }
        //South
        if (direction == 3)
        {
            playersRoomY--;
        }
        //West
        if (direction == 4)
        {
            playersRoomX--;
        }
        GameObject newRoom = RoomGrid[playersRoomX, playersRoomY];
        Debug.Log(playersRoomX + " + " + playersRoomY);
        player.transform.position = newRoom.transform.Find("Spawn").transform.position;
        RoomGrid[playersRoomX, playersRoomY].SetActive(true);

        if (RoomGrid[playersRoomX + 1, playersRoomY] != null)
        {
            east = true;
        }
        if (RoomGrid[playersRoomX, playersRoomY -1] != null)
        {
            south = true;
        }
        if (RoomGrid[playersRoomX-1, playersRoomY] != null)
        {
            west = true;
        }
        if (RoomGrid[playersRoomX, playersRoomY + 1] != null)
        {
            north = true;
        }
        TeleportersSet(playersRoomX, playersRoomY, north, east, south, west);
    }

    void PostSpawn()
    {
        GameObject compare;
        compare = RoomGrid[0, 0];
        for (int k = 0; k < roomGrid.GetLength(0); k++)
        {
            for (int l = 0; l < roomGrid.GetLength(1); l++)
            {
                if (compare == roomGrid[k,l])
                {
                    Destroy(roomGrid[k, l]);
                }
            }
        }
    }

    void SpecialSpawn(GameObject room)
    {
        bool placed = false;
        while (placed == false)
        {
            validSpawn = false;
            bool adjacent = false;
            int i = Random.Range(1, height - 1);
            int j = Random.Range(1, width - 1);

            if (RoomGrid[j, i] != room)
            {
                tileEmpty = true;
            }

            if (RoomGrid[j + 1, i] != null && tileEmpty == true && adjacent == false)
            {
                validSpawn = true;
                adjacent = true;
            }
            else
            {
                validSpawn = false;
            }

            if (RoomGrid[j - 1, i] != null && tileEmpty == true && adjacent == false)
            {
                validSpawn = true;
                adjacent = true;
            }
            else
            {
                validSpawn = false;
            }

            if (RoomGrid[j, i + 1] != null && tileEmpty == true && adjacent == false)
            {
                validSpawn = true;
                adjacent = true;
            }
            else
            {
                validSpawn = false;
            }

            if (RoomGrid[j, i - 1] != null && tileEmpty == true && adjacent == false)
            {
                validSpawn = true;
                adjacent = true;
            }
            else
            {
                validSpawn = false;
            }
            if (validSpawn == true)
            {
                RoomGrid[j, i] = Instantiate(room, new Vector3((j * 20) + j, 0, (i * 20) + i), Quaternion.identity, roomStore.transform);
                RoomGrid[j, i].SetActive(false);
                placed = true;
            }
        }
    }
}
