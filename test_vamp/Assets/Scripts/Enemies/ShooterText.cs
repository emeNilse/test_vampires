using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterText : MonoBehaviour
{
    public void DestroyParent()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        Destroy(parent);
    }
}
