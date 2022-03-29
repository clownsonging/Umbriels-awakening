using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;
    [SerializeField] private GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomBoss();
    }

    void SpawnRandomBoss()
    {
        int i = Random.Range(0, bosses.Length);
        boss = Instantiate(bosses[i], this.transform.position, Quaternion.identity);
    }
}
