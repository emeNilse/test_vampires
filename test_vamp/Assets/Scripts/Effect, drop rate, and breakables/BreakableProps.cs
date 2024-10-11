using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;
    private DropRateManager dropRateManager;

    private void Awake()
    {
        dropRateManager = GetComponent<DropRateManager>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Break();
            dropRateManager.OnDespawn();
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }
}
