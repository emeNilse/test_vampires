using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EnemyEditor : EditorWindow
{
    string enemyBaseName = "";
    [Range(0, 100)]float dropRate = 1.0f;
    Sprite enemyIcon;
    enemyType addedType;
    float health;
    float damage;
    float speed;
    
    public enum enemyType
    {
        Melee,
        Ranged
    }

    
    
    [MenuItem("Tools/EnemyEditor")]
    public static void StartWindow()
    {
        GetWindow(typeof(EnemyEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create a new enemy");

        enemyBaseName = EditorGUILayout.TextField("Base Name", enemyBaseName);

        if (string.IsNullOrEmpty(enemyBaseName)) return;

        else if (CheckExists())
        {
            GUILayout.Label("Name already in use");
            return;
        }

        dropRate = EditorGUILayout.FloatField("Drop Rate, 100 Max", dropRate);

        enemyIcon = EditorGUILayout.ObjectField("Select a Sprite", enemyIcon, typeof(Sprite), false) as Sprite;

        if (enemyIcon == null) return;

        addedType = (enemyType)EditorGUILayout.EnumPopup("Enemy type", addedType);

        health = EditorGUILayout.FloatField("Enemy Health", health);
        damage = EditorGUILayout.FloatField("Enemy Damage", damage);
        speed = EditorGUILayout.FloatField("Enemy Speed", speed);
        
        if(GUILayout.Button("Create Enemy"))
        {
            CreateEnemy();
        }
    }

    bool CheckExists()
    {
        if (File.Exists(enemyBaseName))
        {
            return true;
        }
        return false;
    }

    void CreateEnemy()
    {
        EnemyScriptableObject enemySO = CreateInstance<EnemyScriptableObject>();
        enemySO.maxHealth = health;
        enemySO.moveSpeed = speed;
        enemySO.damage = damage;

        AssetDatabase.CreateAsset(enemySO, "Assets/Scriptable Objects/Enemies/" + enemyBaseName + ".asset");



        AssetDatabase.CopyAsset("Assets/Prefabs/Enemies/EnemyBase.prefab", "Assets/Prefabs/Enemies/" + enemyBaseName + ".prefab");

        GameObject newEnemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemies/" + enemyBaseName + ".prefab");

        newEnemy.AddComponent<MeleeEnemy>();
        newEnemy.GetComponent<MeleeEnemy>().enemyData = enemySO;
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyIcon;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
