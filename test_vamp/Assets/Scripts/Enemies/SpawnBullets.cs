using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullets : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletParent;
    //Create bullet list 
    List<BulletMovement> bullets = new List<BulletMovement>();

    public void SpawnBullet()
    {
        GameObject BulletToSpawn = bullet;

        foreach (BulletMovement b in bullets)
        {
            if (!b.isActiveAndEnabled)
            {
                b.RespawnBullet(bulletParent.transform.position);
                return;
            }
        }
        GameObject e = Instantiate(BulletToSpawn, bulletParent.transform.position, Quaternion.identity);
        bullets.Add(e.GetComponent<BulletMovement>());
    }
}
