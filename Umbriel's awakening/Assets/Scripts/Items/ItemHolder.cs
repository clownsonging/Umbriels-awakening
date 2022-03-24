using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] itemList;
    [SerializeField] private GameObject[] rareItemList;
    [SerializeField] private GameObject spawnLocation;
    private GameObject itemSpawned;
    private bool pickUp = false;

    private void Start()
    {
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
        Instantiate(itemSpawned, spawnLocation.transform.position, Quaternion.identity);
    }
}
