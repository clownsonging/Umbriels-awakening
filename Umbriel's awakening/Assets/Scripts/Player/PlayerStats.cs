using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField] private StatUI UI;
    [SerializeField] private int hp = 100;
    [SerializeField] private int currentHp = 100;
    [SerializeField] private float attackSpeed = .1f;
    [SerializeField] private int gold = 30;
    [SerializeField] private int bombs = 3;
    [SerializeField] private float speed = 1;
    [SerializeField] private float range = 10;
    [SerializeField] private float damage = 10;

    [Header("Room variables")]
    [SerializeField] private int enemiesLeft = 1;
    [SerializeField] private GameObject generationContainer;

    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Gold { get => gold; set => gold = value; }
    public int EnemiesLeft { get => enemiesLeft; set => enemiesLeft = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float Speed { get => speed; set => speed = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int Bombs { get => bombs; set => bombs = value; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        generationContainer = GameObject.FindGameObjectWithTag("Gen");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        generationContainer = GameObject.FindGameObjectWithTag("Gen");
    }
    // Update is called once per frame
    void Update()
    {
        RoomClear();
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

    public void DealDamage(int i)
    {
        currentHp = currentHp - i;
        UI.UpdateUI();
    }
}
