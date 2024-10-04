using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Must be in a folder called Editor of UnityEditor doesn't work
public class UpgradeEditor : EditorWindow
{
    [MenuItem("Tools/UpgradeEditor")]

    public static void StartWindow()
    {
        GetWindow<UpgradeEditor>();
    }

    string UpgradeName = "";

    private void OnGUI()
    {
        GUILayout.Label("Upgrade Editor", EditorStyles.boldLabel);

        UpgradeName = GUILayout.TextField(UpgradeName);

        if(string.IsNullOrEmpty(UpgradeName))
        {
            GUILayout.Label("Please give the upgrade a name");
            return;
        }
        
        GUILayout.BeginHorizontal();

        
        if(GUILayout.Button("Create Upgrade"))
        {
            CreateObjectInAssests();
            Debug.Log("cool bruh");
        }
        GUILayout.EndHorizontal();
    }

    void CreateObjectInAssests()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        UpgradeName = "";
    }

}
