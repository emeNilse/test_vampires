using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletMovement : MonoBehaviour
{
    GameObject target;
    public WeaponsScriptableObjects weaponData;
    Rigidbody2D bulletRB;
    Vector2 moveDir;

    public void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDir = (target.transform.position - transform.position).normalized * weaponData.Speed;
        bulletRB.velocity = moveDir;
        Destroy(this.gameObject, 2);
    }
    public void Update() //this is how I stop the bullets in pause without timescale
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0f)
        {
            bulletRB.velocity = new Vector2(0f, 0f);
        }
    }

    public float GetDamage()
    {
        int level = GameManager.Instance.GetLevel();
        float damage = weaponData.Damage * (1 + (level / 3));
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.takeDamage(GetDamage()); //make sure to use currentDamage in case of damage multipliers
            Destroy(this.gameObject);
        }
    }
}
