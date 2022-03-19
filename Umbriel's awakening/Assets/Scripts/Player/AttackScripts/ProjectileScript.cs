using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float life = 5;
    [SerializeField] int damage = 2;

    public int Damage { get => damage; set => damage = value; }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, .02f);
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(this.gameObject, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            collision.gameObject.GetComponent<BaseEnemyAI>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
