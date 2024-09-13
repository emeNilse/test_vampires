using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowBehaviour : MonoBehaviour
{

    [SerializeField] Transform bowParent;
    public GameObject arrow;
    public float fireRate = 1f;
    private float nextFireTime;

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowDir = mousePos - (Vector2)bowParent.position;
        bowParent.up = bowDir.normalized;

        if(Input.GetMouseButtonDown(0) && nextFireTime < Time.time)
        {
            Instantiate(arrow, bowParent.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }


    }
}
