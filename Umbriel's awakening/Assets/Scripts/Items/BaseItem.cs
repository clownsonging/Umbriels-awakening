using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;

    [Header("Item attributes")]
    [SerializeField] private string name;
    [SerializeField] private float hp;
    [SerializeField] private float hpCap;
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private string flavourText;
    [SerializeField] private ItemHolder holder;

    void Start()
    {
        holder = this.GetComponentInParent<ItemHolder>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    public BaseItem()
    {
        name = "GenericItem";
        hp = 1.1f;
        speed = 1.1f;
        attackSpeed = 1.1f;
        range = 1.1f;
        damage = 1.1f;
        flavourText = "Well atleast this works... Maybe?";
    }

    public void Update()
    {
        this.transform.Rotate(Vector3.up * Time.deltaTime * 10);
    }

    public BaseItem(string n, float h, float s, float aS, float r, float d, string fT)
    {
        name = n;
        hp = h;
        speed = s;
        attackSpeed = aS;
        range = r;
        damage = d;
        flavourText = fT;
    }

    public void Apply()
    {
        playerStats.CurrentHp = Mathf.RoundToInt(playerStats.CurrentHp * hp);
        playerStats.Hp = Mathf.RoundToInt(playerStats.Hp * hpCap);
        playerStats.Speed = playerStats.Speed * speed;
        playerStats.AttackSpeed = playerStats.AttackSpeed * attackSpeed;
        playerStats.Range = playerStats.Range * range;
        playerStats.Damage = playerStats.Damage * damage;
        holder.PickUp();
    }

    public void OnTriggerEnter()
    {
        Apply();
        Destroy(this.gameObject);
    }
}
