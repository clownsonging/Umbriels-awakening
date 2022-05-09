using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MapUI : MonoBehaviour
{
    [SerializeField] private InputAction mapToggle;

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
    [SerializeField] private GenScript gen;


    private void Start()
    {
        mapToggle.Enable();
    }

    private void Update()
    {
        if(mapToggle.triggered)
        {
            map.enabled = !map.enabled;
        }
        UpdateRoom();
    }

    public void UpdateMap()
    {
        gen = GameObject.FindGameObjectWithTag("Gen").GetComponent<GenScript>();
        panels = new GameObject[gen.Width, gen.Height];
        Debug.Log(gen.Height);
        for (int i = 0; i < gen.Height; i++)
        {
            for(int j = 0; j < gen.Width; j++)
            {
                MakeTile(j, i);
                DarkenRoom(j, i);
            }
        }
    }

    public void DarkenRoom(int j, int i)
    {
        if(panels[j,i] != null)
        {
            panels[j, i].GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void LightRoom(int j, int i)
    {
        if(panels[j,i] != null)
        {
            panels[j, i].GetComponent<CanvasGroup>().alpha = .5f;
        }
    }

    public void CurrentRoom()
    {
        if (panels[gen.PlayerX, gen.PlayerY] != null)
        {
            panels[gen.PlayerX, gen.PlayerY].GetComponent<CanvasGroup>().alpha = 1f;
        }
    }

    private void MakeTile(int j, int i)
    {
        if (gen.FloorGrid[j, i].gameObject.tag == "Room")
        {
            panels[j, i] = Instantiate(basePanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
        }
        if (gen.FloorGrid[j, i].gameObject.tag == "BossRoom")
        {
            panels[j, i] = Instantiate(bossPanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
        }
        if (gen.FloorGrid[j, i].gameObject.tag == "ItemRoom")
        {
            panels[j, i] = Instantiate(itemPanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
        }
        if (gen.FloorGrid[j, i].gameObject.tag == "ShopRoom")
        {
            panels[j, i] = Instantiate(shopPanel, new Vector3(j * x, i * y, 0), Quaternion.identity, map.transform);
        }
    }

    private bool CheckNorth()
    {
        if(panels[gen.PlayerX, gen.PlayerY + 1] != null)
        {
            LightRoom(gen.PlayerX, gen.PlayerY + 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckEast()
    {
        if (panels[gen.PlayerX + 1, gen.PlayerY] != null)
        {
            LightRoom(gen.PlayerX + 1, gen.PlayerY);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckSouth()
    {
        if (panels[gen.PlayerX, gen.PlayerY - 1] != null)
        {
            LightRoom(gen.PlayerX, gen.PlayerY - 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckWest()
    {
        if (panels[gen.PlayerX - 1, gen.PlayerY] != null)
        {
            LightRoom(gen.PlayerX - 1, gen.PlayerY);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateRoom()
    {
        CurrentRoom();
        CheckNorth();
        CheckEast();
        CheckSouth();
        CheckWest();
    }
}
