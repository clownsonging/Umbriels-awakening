using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    [SerializeField] private GameObject[] roomList;
    [SerializeField] private GameObject player;
    [SerializeField] private int playersRoomX;
    [SerializeField] private int playersRoomY;

    [SerializeField] private GameObject[,] roomGrid;
    [SerializeField] private GameObject roomStore;

    private GameObject room;

    private int height;
    private int width;
    private int noRooms;
    private int roomType;
    private bool validSpawn = false;
    private bool tileEmpty;

    public GameObject[,] RoomGrid { get => roomGrid; set => roomGrid = value; }

    // Start is called before the first frame update
    void Start()
    {
        GenerateFloor();
        Teleporters();
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
        playersRoomX = Mathf.RoundToInt(width / 2);
        playersRoomY = Mathf.RoundToInt(height / 2);
        GameObject spawn = Instantiate(room, new Vector3(Mathf.RoundToInt(width / 2) * 20, 0, Mathf.RoundToInt(height / 2) * 20), Quaternion.identity);
        player.transform.position = new Vector3(spawn.transform.position.x + 5, spawn.transform.position.y + 20, spawn.transform.position.z + 1);
        Destroy(spawn);
        noRooms--;
        Debug.Log("Spawned first room at " + width + " width & " + height + " height.");

        player.transform.position = new Vector3(spawn.transform.position.x + 5, spawn.transform.position.y, spawn.transform.position.z + 1);

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
                tileEmpty = false;
            }
        }
    }
    void Teleporters()
    {
        for(int k = 0; k < roomGrid.GetLength(0); k++)
        {
            for(int l = 0; l < roomGrid.GetLength(1); l++)
            {
                if (roomGrid[k,l] != null)
                {
                    bool north = CheckNeighbour(k, l)[0];
                    bool east = CheckNeighbour(k, l)[1];
                    bool south = CheckNeighbour(k, l)[2];
                    bool west = CheckNeighbour(k, l)[3];

                    RoomNavigation temp = roomGrid[k, l].GetComponent<RoomNavigation>();
                    temp.Portals(north, east, south, west);
                    //Debug.Log("K: " + k + " L: " + l);
                }
            }
        }
    }

    public void newRoom(int direction)
    {
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
            playersRoomY--;
        }
        GameObject newRoom = roomGrid[playersRoomX, playersRoomY];
        player.transform.position = newRoom.transform.Find("Spawn").transform.position;
    }
}
