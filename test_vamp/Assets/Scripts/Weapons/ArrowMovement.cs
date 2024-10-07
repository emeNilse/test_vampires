using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    Rigidbody2D arrowRB;
    public WeaponsScriptableObjects weaponData;
    private int currentPierce; 

    void Start()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        currentPierce = weaponData.Pierce;

        // Attempt to make arrow move via guessed methods from first lecture, need to ask how this can work
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos.z = 0;
            //Vector3 arrowDir = mousePos - transform.position;
            //transform.position += arrowDir * weaponData.Speed * Time.deltaTime; //set the movement of the knife
            //arrowRB.velocity = arrowDir;//transform.position += arrowDir * weaponData.Speed * Time.deltaTime; //set the movement of the knife
            //arrowRB.velocity = arrowDir;

        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection.z = 0;
        arrowRB.velocity = new Vector2(shootDirection.x * weaponData.Speed, shootDirection.y * weaponData.Speed);
        transform.up = shootDirection.normalized;


        Destroy(this.gameObject, 2);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(weaponData.Damage);
            Effects.SpawnBloodFX(transform.position);
            ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(weaponData.Damage);
                ReducePierce();
            }
        }
    }

    void ReducePierce() //destroy once pierce reaches 0, as in hiting a target
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
