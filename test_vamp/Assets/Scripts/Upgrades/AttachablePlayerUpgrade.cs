using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AttachablePlayerUpgrade : MonoBehaviour
{
    public enum AttachedObjectBehaviour
    {
        None,
        PointToMouse,
        OrbitPlayer,
    }

    public AttachedObjectBehaviour myBehaviour;
    public float myDistance = 0;
    Player myPlayer;

    public void Initialize(Player aPlayer)
    {
        myPlayer = aPlayer;
    }

    public void UpdateAttachable(int iteration)
    {
        switch (myBehaviour)
        {
            case AttachedObjectBehaviour.None:
                break;
            case AttachedObjectBehaviour.PointToMouse:

                break;
            case AttachedObjectBehaviour.OrbitPlayer:
                transform.localPosition = new Vector3(
                    Mathf.Cos((Time.time + Mathf.PI + (Mathf.PI / iteration))) * myDistance,
                    Mathf.Sin((Time.time + Mathf.PI + (Mathf.PI / iteration))) * myDistance, 0);

                break;
        }
    }
}
