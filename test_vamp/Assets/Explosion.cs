using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float life = 0.3F;
    void Start()
    {
        Destroy(gameObject, life);
    }

    
}
