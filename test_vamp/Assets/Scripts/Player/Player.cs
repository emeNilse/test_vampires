using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PlayerStats
{
    // References
    Rigidbody2D rb;
    //public CharacterScriptableObject characterData;
    [SerializeField] SwordController sword;
    [SerializeField] CrossBowBehaviour crossbow;


    //Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;
    //[SerializeField] Transform swordParent;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); //so that knife has movement at start of game and if player doesn't move
    }
    
    public void PlayerUpdate()
    {
        InputManagement();
        sword.SwordUpdate();
        crossbow.CrossBowUpdate();
        


        //SwordRotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); // last moved x
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); //last moved y
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); //while moving
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
            //SwordRotate();
        //}
        
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * currentMoveSpeed, moveDir.y * currentMoveSpeed);
    }

    //void SwordRotate()
    //{
      //  swordParent.Rotate(-Vector3.forward, 360 * Time.deltaTime);
    //}
}
