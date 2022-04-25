using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float life = 5;
    [SerializeField] int damage = 10;

    public int Damage { get => damage; set => damage = value; }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, .08f);
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(this.gameObject, 0);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            try
            {
                collision.gameObject.GetComponent<PlayerStats>().DealDamage(damage);
                Destroy(this.gameObject);
            }
            catch
            {
            }
        }
    }
}
