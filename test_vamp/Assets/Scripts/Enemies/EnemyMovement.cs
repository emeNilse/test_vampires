using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;

    void Start()
    {
        player = FindObjectOfType<playermovement>().transform;
    }

   
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime); //constantly move enemy towards player
    }
}
