using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public static void SpawnDeathExplosionFX(Vector3 aPosition)
    {
        Instantiate(Resources.Load<GameObject>("Explosion"), aPosition, Quaternion.identity);
    }

    public static void SpawnDeathFireWorkFX(Vector3 aPosition)
    {
        Instantiate(Resources.Load<GameObject>("FireWorkParticles"), aPosition, Quaternion.identity);
    }
}
