using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeMenu : MonoBehaviour
{

    public UnityEvent OnUpgrade;
    public UnityEvent OnMightUpgrade;
    public UnityEvent OnRecoveryUpgrade;
   
    public void ChooseMightUpgrade()
    {
        OnMightUpgrade.Invoke();
        OnUpgrade.Invoke();
    }

    public void ChooseRecoveryUpgrade()
    {
        OnRecoveryUpgrade.Invoke();
        OnUpgrade.Invoke();
    }
}
