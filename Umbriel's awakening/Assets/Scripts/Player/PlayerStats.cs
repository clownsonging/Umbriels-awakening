using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int gold = 30;
    [SerializeField] private int bombs = 3;
    [SerializeField] private int enemiesLeft = 1;
    [SerializeField] private GameObject generationContainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RoomClear();
    }

    void RoomClear()
    {
        if(enemiesLeft == 0)
        {
            generationContainer.GetComponent<Generation>().RoomCleared();
            enemiesLeft = -1;
        }
    }

    public void NewRoom(RoomNavigation room)
    {
        enemiesLeft = room.EnemyCount();
    }
}
