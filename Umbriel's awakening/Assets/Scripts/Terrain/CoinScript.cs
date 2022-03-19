using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private int value;
   public int Hit()
    {
        Destroy(this.gameObject);
        return value;
    }
}
