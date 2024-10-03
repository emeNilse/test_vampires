using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : State
{
    [SerializeField] GameObject myPauseCanvas;

    public override void UpdateState()
    {
        base.UpdateState();

        myPauseCanvas.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.SwitchState<PlayingState>();
        }
    }
}
