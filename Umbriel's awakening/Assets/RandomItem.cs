using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject spawn;
    GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(-1, items.Length);
        item = Instantiate(items[i], spawn.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        item.transform.Rotate(Vector3.up * Time.deltaTime*10);
    }
}
