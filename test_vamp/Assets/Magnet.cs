using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ExperienceGem gem))
        {
            gem.SetTarget(transform.parent.position);
        }

        if (collision.gameObject.TryGetComponent(out HealthPotion potion))
        {
            potion.SetTarget(transform.parent.position);
        }
    }
}
