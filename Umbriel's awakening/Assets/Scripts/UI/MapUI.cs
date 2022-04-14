using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [Header("Positioning variables")]
    [SerializeField] private int x;
    [SerializeField] private int y;
    [Header("Panels")]
    [SerializeField] private GameObject basePanel;
    [SerializeField] private GameObject bossPanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Canvas map;
    private GameObject[,] panels;
    private GenScript gen;


    private void Start()
    {
        gen = GameObject.FindGameObjectWithTag("Gen").GetComponent<GenScript>();
    }

    public void UpdateMap()
    {
        for (int i = 0; i < gen.Height; i++)
        {
            for(int j = 0; j < gen.Width; j++)
            {
                if(gen.FloorGrid[j,i].gameObject.tag == "Room")
                {
                   Instantiate(basePanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
                }
                if (gen.FloorGrid[j, i].gameObject.tag == "BossRoom")
                {
                    Instantiate(bossPanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
                }
                if (gen.FloorGrid[j, i].gameObject.tag == "ItemRoom")
                {
                    Instantiate(itemPanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
                }
                if (gen.FloorGrid[j, i].gameObject.tag == "ShopRoom")
                {
                    Instantiate(shopPanel, new Vector3(j * 120, i * 120, 0), Quaternion.identity, map.transform);
                }
            }
        }
    }

    public void LightRoom(int x, int y)
    {
        panels[x, y].SetActive(true);
    }
}
