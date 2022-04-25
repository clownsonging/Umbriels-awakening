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
        player = GameObject.FindGameObjectWithTag("Player");
        roomSpawn = room.GetComponentInChildren<RoomNavigation>().Spawn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = roomSpawn.transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<BaseEnemyAI>().Bonk();
        }
    }
}
