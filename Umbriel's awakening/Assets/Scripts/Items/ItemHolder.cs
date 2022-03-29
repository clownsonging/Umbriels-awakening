using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] itemList;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] rareItemList;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private int cost;
    private PlayerStats stats;
    private GameObject itemSpawned;
    private bool pickUp = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
        int chance = Random.Range(0, 10);
        if (chance > 7)
        {
            cost = Mathf.RoundToInt(cost / 2);
        }
        SpawnItem();
    }
    void SpawnItem()
    {
        int i = Random.Range(0, itemList.Length);
        itemSpawned = Instantiate(itemList[i], spawnLocation.transform.position, Quaternion.identity, this.gameObject.transform);
        Debug.Log("Item room spawned: " + itemSpawned.name);
    }

    void SpawnRareItem()
    {
        int i = Random.Range(0, rareItemList.Length);
        itemSpawned = itemList[i];
        Instantiate(itemSpawned, spawnLocation.transform.position, new Quaternion(0,0,-90,0));
    }

    public void PickUp()
    {
        stats.Gold = stats.Gold - cost;
        Debug.Log("picked up: " + itemSpawned.name + " for " + cost + " gold.");
    }
}
