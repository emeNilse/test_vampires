using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{

    public int experienceGranted;
    Rigidbody2D rb;
    Vector3 targetPosition;
    bool hasTarget;
    int level;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * 5f;
        }
    }

    public void SetTarget(Vector3 pos)
    {
        targetPosition = pos;
        hasTarget = true;
    }

}
