using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    //public GameObject player;
    public float moveSpeed;
    //private float distance;
    void Start()
    {
       player = FindObjectOfType<playermovement>().transform;
    }

   
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime); //constantly move enemy towards player
        
        //distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.forward;
    }
}
