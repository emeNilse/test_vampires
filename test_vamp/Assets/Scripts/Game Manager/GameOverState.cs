using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    [SerializeField] GameObject myGameOverCanvas;
    [SerializeField] Player myPlayer;
    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 aPos = new Vector3(myPlayer.transform.position.x, myPlayer.transform.position.y);
        //Effects.SpawnPlayerBloodFX(aPos);
        myGameOverCanvas.SetActive(true);
    }
}
