using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class cloudMovement : MonoBehaviour
{
    float randomSpeed;
    float randomScale;
    Vector3 changeScale;

    void Start()
    {
        randomSpeed = Random.Range(1, 5);
        randomScale = Random.Range(0.3f, 1.5f);
        changeScale = new Vector3(randomScale, randomScale, randomScale);
        transform.localScale = changeScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * randomSpeed);
    }

    void DespawnCloud()
    {
        gameObject.SetActive(false);
    }

    public void RespawnCloud(Vector3 aPosition)
    { 
        gameObject.SetActive (true);
        transform.position = aPosition;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("EndPoint"))
        {
            DespawnCloud();
        }
    }
}
