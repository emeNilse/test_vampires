using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    [SerializeField] GameObject myGameOverCanvas;

    public override void UpdateState()
    {
        base.UpdateState();

        myGameOverCanvas.SetActive(true);
    }
}
