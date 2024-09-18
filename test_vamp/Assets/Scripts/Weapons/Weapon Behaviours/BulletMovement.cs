using System.Collections;
using System.Collections.Generic;
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
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0f)
        {
            bulletRB.velocity = new Vector2(0f, 0f);
            //Time.timeScale = 0f;
        }
        //else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0f)
        //{
          //  bulletRB.velocity = moveDir;
           // Time.timeScale = 1f;
        //}
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
