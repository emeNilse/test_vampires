using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PlayerStats
{
    // References
    Rigidbody2D rb;
    [SerializeField] SwordController sword;
    [SerializeField] CrossBowBehaviour crossbow;

    //upgrade add ons, need further inspection
    [SerializeField] Transform AttachedPointingTransform; //transform for attachable weapons
    List<AttachablePlayerUpgrade> attachedUpgrades = new List<AttachablePlayerUpgrade>();
    public Vector3 MouseDir { get; private set; }


    //Movement
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;
    
    
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

        //attachable update methods
        SetMouseDir();
        UpdateAttachables();

        //SwordRotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // mouse direction for attachables
    void SetMouseDir()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseDir = mousePos - (Vector2)transform.position; //why transform?

        AttachedPointingTransform.up = MouseDir;
    }

    //updating attachables
    void UpdateAttachables()
    {
        for (int i = 0; i < attachedUpgrades.Count; i++)
        {
            attachedUpgrades[i].UpdateAttachable(i);
        }
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


    // new
    public void UpgradePlayer(UpgradeScriptableObjects anUpgrade)
    {
        if (anUpgrade.attachPrefabs.Count > 0)
        {
            foreach (GameObject attachedPrefab in anUpgrade.attachPrefabs)
            {
                if (anUpgrade.attachPrefabs != null)
                {
                    AttachablePlayerUpgrade p = attachedPrefab.GetComponent<AttachablePlayerUpgrade>();

                    GameObject spawnedObject = null;

                    switch (p.myBehaviour)
                    {
                        case AttachablePlayerUpgrade.AttachedObjectBehaviour.None:
                            return;
                        case AttachablePlayerUpgrade.AttachedObjectBehaviour.PointToMouse:

                            spawnedObject = Instantiate(attachedPrefab, AttachedPointingTransform);
                            spawnedObject.transform.localPosition = attachedPrefab.transform.position;

                            break;
                        case AttachablePlayerUpgrade.AttachedObjectBehaviour.OrbitPlayer:

                            spawnedObject = Instantiate(attachedPrefab, transform);
                            spawnedObject.transform.localPosition = Vector3.zero;

                            break;
                    }

                    if (spawnedObject)
                    {
                        AttachablePlayerUpgrade addedUpgrade = spawnedObject.GetComponent<AttachablePlayerUpgrade>();
                        addedUpgrade.Initialize(this);
                        attachedUpgrades.Add(addedUpgrade);
                    }
                }
            }
        }

        currentMoveSpeed += anUpgrade.Speed;
        currentMight += anUpgrade.Damage;

        if (anUpgrade.WeaponSize > 0)
        {
            foreach (AttachablePlayerUpgrade p in attachedUpgrades)
            {
                p.transform.localScale += Vector3.one * anUpgrade.WeaponSize;
            }
        }
    }
}
