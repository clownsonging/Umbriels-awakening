using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject roomSpawn;
    private void Start()
    {
        roomSpawn = room.GetComponentInChildren<RoomNavigation>().Spawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = roomSpawn.transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            try
            {
                other.gameObject.GetComponent<BaseEnemyAI>().Bonk();
            }
            catch
            {
                other.gameObject.GetComponent<BaseRangedEnemy>().Bonk();
            }
        }
    }
}
