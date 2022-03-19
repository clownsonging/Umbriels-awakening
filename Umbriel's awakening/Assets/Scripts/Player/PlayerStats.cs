using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int gold = 30;
    [SerializeField] private int bombs = 3;
    [SerializeField] private int enemiesLeft = 1;
    [SerializeField] private GameObject generationContainer;
    [SerializeField] private float fireRate = .1f;
    [SerializeField] private Text hpText;
    [SerializeField] private Text goldText;
    [SerializeField] private Text enemiesText;
    [SerializeField] private Text bombText;

    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Gold { get => gold; set => gold = value; }
    public int EnemiesLeft { get => enemiesLeft; set => enemiesLeft = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RoomClear();
        updateUI();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            gold = gold + collision.gameObject.GetComponent<CoinScript>().Hit();
        }
    }
    void RoomClear()
    {
        if(enemiesLeft == 0)
        {
            generationContainer.GetComponent<GenScript>().RoomClear();
            enemiesLeft = -1;
        }
    }

    public void NewRoom(RoomNavigation room)
    {
        enemiesLeft = room.EnemyCount();
    }

    void updateUI()
    {
        hpText.text = "Health: " + hp;
        goldText.text = "Gold: " + gold;
        enemiesText.text = "Enemies Left:" + enemiesLeft;
        bombText.text = "Bombs: " + bombs;
    }
}
