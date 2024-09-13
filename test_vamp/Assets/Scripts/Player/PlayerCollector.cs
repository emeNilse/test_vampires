using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        //checks if there is a collectible to collect
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //if yes, calls collect method
            collectible.Collect();
        }
    }
}
