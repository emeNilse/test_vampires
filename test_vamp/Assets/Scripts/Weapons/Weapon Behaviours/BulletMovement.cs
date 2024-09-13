using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    GameObject target;
    public WeaponsScriptableObjects weaponData;
    Rigidbody2D bulletRB;


    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * weaponData.Speed;
        bulletRB.velocity = moveDir;
        Destroy(this.gameObject, 2);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.takeDamage(weaponData.Damage); //make sure to use currentDamage in case of damage multipliers
            Destroy(this.gameObject);
        }
    }
}
