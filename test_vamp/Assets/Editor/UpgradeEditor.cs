using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.WSA;
using System;



public class UpgradeEditor : EditorWindow
{
    string UpgradeName = "";
    string UpgradeDescription = "";
    const string UpgradeDataPath = "Assets/GameData/Upgrades/";
    const string AttatchableDataPath = "Assets/Prefabs/AttatchableUpgradesPrefabs/";

    UpgradeType upgradeAddType;
    StatType statAddType;

    Sprite texture_upgradeIcon;
    Texture2D texture_playerReference;
    Texture2D texture_dot;

    List<UpgradeView> addedUpgradeTypes = new List<UpgradeView>();
    List<UpgradeScriptableObjects> upgradesInAssets = new List<UpgradeScriptableObjects>();

    Material default_SpriteMaterial;

    UpgradeScriptableObjects currentEditing = null;

    Vector2 scrollPos = Vector2.zero;

    int selected = 0;

    public class UpgradeView
    {
        public UpgradeView()
        {
            stats = new Dictionary<StatType, float>();
            attatchObject = null;
        }
        public UpgradeView(UpgradeType aType, Dictionary<StatType, float> someStats, GameObject anAttatchObject)
        {
            type = aType;
            stats = someStats;
            attatchObject = anAttatchObject;
        }

        public UpgradeType type;
        public Dictionary<StatType, float> stats;
        public GameObject attatchObject;
    }

    public enum StatType
    {
        Damage,
        Speed,
        WeaponSize,
        OrbitRadius,
        Health
    }
    public enum UpgradeType
    {
        AttatchToPlayer,
        AddStats
    }

    [MenuItem("Tools/UpgradeEditor")]
    public static void StartWindow()
    {
        UpgradeEditor e = GetWindow<UpgradeEditor>();
        e.minSize = new Vector2(600, 300);
        e.Show();

        e.titleContent = new GUIContent("Upgrade Editor", "Change and edit upgrades");
    }

    private void OnGUI()
    {
        selected = EditorGUI.Popup(EditorGUILayout.GetControlRect(), selected, upgradesInAssets.Select(it => it.name).ToArray());

        if (!currentEditing)
        {
            if (GUILayout.Button("Load selected upgrade"))
            {
                LoadUpgrade(upgradesInAssets[selected]);
            }
        }
        else
        {
            if (GUILayout.Button("Create new upgrade"))
            {
                ClearData();
            }
        }

        EditorMain();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Rebuild UpgradeManager"))
        {
            PopulateUpgradeManager();
        }
        EditorGUILayout.EndHorizontal();
    }

    void EditorMain()
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Please select an icon and name for your upgrade", EditorStyles.boldLabel);
        EditorGUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Select Name - ");
        UpgradeName = GUILayout.TextField(UpgradeName);
        EditorGUILayout.EndHorizontal();

        if (string.IsNullOrEmpty(UpgradeName)) return;

        EditorGUILayout.BeginHorizontal();
        texture_upgradeIcon = (Sprite)EditorGUILayout.ObjectField("Select Icon - ", texture_upgradeIcon, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        if (texture_upgradeIcon == null) return;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Describe your upgrade - ");
        UpgradeDescription = GUILayout.TextField(UpgradeDescription);
        EditorGUILayout.EndHorizontal();

        if (string.IsNullOrEmpty(UpgradeDescription)) return;

        else if (CheckExists() && !currentEditing)
        {
            GUILayout.Label("Upgrade name already in use!");
            return;
        }

        EditorGUILayout.BeginHorizontal();

        upgradeAddType = (UpgradeType)EditorGUILayout.EnumPopup("Upgrade type - ", upgradeAddType);

        if (GUILayout.Button("Add Upgrade"))
        {
            AddUpgradeToView();
        }

        List<UpgradeView> upgradeLoopThrough = new List<UpgradeView>();
        upgradeLoopThrough.AddRange(addedUpgradeTypes);

        EditorGUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int i = 0; i < addedUpgradeTypes.Count; i++)
        {
            EditorGUILayout.Space(40);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Upgrade - " + i, EditorStyles.boldLabel);

            if (GUILayout.Button("Remove"))
            {
                addedUpgradeTypes.RemoveAt(i);
                break;
            }

            EditorGUILayout.Space(20);
            EditorGUILayout.EndHorizontal();

            UpgradeView upgradeView = addedUpgradeTypes[i];

            switch (addedUpgradeTypes[i].type)
            {
                case UpgradeType.AttatchToPlayer:

                    AttatchToPlayerUI(upgradeView);


                    break;
                case UpgradeType.AddStats:

                    AddStatUI(upgradeView);

                    break;
            }
        }
        EditorGUILayout.EndScrollView();


        EditorGUILayout.Space(100);


        EditorGUILayout.BeginHorizontal();

        if (!currentEditing)
        {
            if (GUILayout.Button("Create Upgrade"))
            {
                CreateObjectInAssets();
            }
        }
        else
        {
            if (GUILayout.Button("Save Upgrade"))
            {
                CreateObjectInAssets();
            }
        }

        EditorGUILayout.EndHorizontal();

    }


    void AddUpgradeToView()
    {
        foreach (UpgradeView u in addedUpgradeTypes)
        {
            if (u.type == UpgradeType.AddStats && upgradeAddType == UpgradeType.AddStats)
            {
                EditorGUILayout.EndHorizontal();
                return;
            }
        }

        UpgradeView v = new UpgradeView();

        v.type = upgradeAddType;
        v.stats = new Dictionary<StatType, float>();
        v.attatchObject = null;

        addedUpgradeTypes.Add(v);
    }

    void AttatchToPlayerUI(UpgradeView upgradeView)
    {
        EditorGUILayout.BeginHorizontal();
        upgradeView.attatchObject = EditorGUILayout.ObjectField("Attatch object", upgradeView.attatchObject, typeof(GameObject), false) as GameObject;

        if (upgradeView.attatchObject == null)
        {
            if (GUILayout.Button("Create new"))
            {
                Debug.Log("Pressed bhutan"); // funny bug wth
                GameObject newObject = CreateNewAttachable();
                if (newObject != null)
                {
                    upgradeView.attatchObject = newObject;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.EndHorizontal();
            if (!upgradeView.attatchObject.GetComponent<AttachablePlayerUpgrade>())
            {
                Debug.LogWarning(" [UpgradeEditor] Attatched object does not contain 'AttatchablePlayerUpgrade' ");
                upgradeView.attatchObject = null;
                return;
            }
            EditAttatchable(upgradeView.attatchObject);

        }
    }

    void EditAttatchable(GameObject anAttatchable)
    {
        EditorGUILayout.BeginHorizontal();
        AttachablePlayerUpgrade curUpgrade = anAttatchable.GetComponent<AttachablePlayerUpgrade>();
        SpriteRenderer curRend = anAttatchable.GetComponent<SpriteRenderer>();

        curUpgrade.myBehaviour = (AttachablePlayerUpgrade.AttachedObjectBehaviour)EditorGUILayout.EnumPopup("Object behavior", curUpgrade.myBehaviour);
        curRend.sprite = (Sprite)EditorGUILayout.ObjectField("Select Icon - ", curRend.sprite, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        switch (curUpgrade.myBehaviour)
        {
            case AttachablePlayerUpgrade.AttachedObjectBehaviour.OrbitPlayer:

                float maxDist = 7.0f;
                float t = curUpgrade.myDistance / maxDist;

                Rect playerRect = GUILayoutUtility.GetLastRect();
                playerRect.width = 100;
                playerRect.height = 100;
                playerRect.x += 50;
                playerRect.y += 50;
                EditorGUI.DrawPreviewTexture(playerRect, texture_playerReference, default_SpriteMaterial);

                Rect orbiterRect = playerRect;

                orbiterRect.width = 100;
                orbiterRect.height = 100;
                orbiterRect.x += 64 * Mathf.Lerp(0, maxDist, t);
                EditorGUI.DrawPreviewTexture(orbiterRect, curRend.sprite.texture, default_SpriteMaterial);

                EditorGUILayout.Space(100);

                curUpgrade.myDistance = EditorGUILayout.Slider(curUpgrade.myDistance, 0.1f, maxDist);

                break;
            case AttachablePlayerUpgrade.AttachedObjectBehaviour.PointToMouse:

                Rect SpriteRect = GUILayoutUtility.GetLastRect();
                SpriteRect.y += 50;
                SpriteRect.x += 50;
                SpriteRect.width = 100;
                SpriteRect.height = 100;
                EditorGUI.DrawPreviewTexture(SpriteRect, curRend.sprite.texture, default_SpriteMaterial);

                Rect PivotRect = SpriteRect;
                PivotRect.width = 10;
                PivotRect.height = 10;
                PivotRect.x += 50 + (curUpgrade.transform.position.x * 50) - PivotRect.width / 2;
                PivotRect.y += 50 + ((curUpgrade.transform.position.y * -1) * 50 - PivotRect.height / 2);

                default_SpriteMaterial.color = Color.red;
                EditorGUI.DrawPreviewTexture(PivotRect, texture_dot, default_SpriteMaterial);
                default_SpriteMaterial.color = Color.white;

                EditorGUILayout.Space(100);

                EditorGUILayout.LabelField("Pivot");

                Vector3 attatchPos = curUpgrade.transform.position;

                attatchPos.x = EditorGUILayout.FloatField("pivot x", Mathf.Clamp(attatchPos.x, -1, 1));
                attatchPos.y = EditorGUILayout.FloatField("pivot y", Mathf.Clamp(attatchPos.y, -1, 1));

                curRend.transform.position = attatchPos;

                break;
        }
    }

    void AddStatUI(UpgradeView upgradeView)
    {
        upgradeView.attatchObject = null;

        EditorGUILayout.BeginHorizontal();
        statAddType = (StatType)EditorGUILayout.EnumPopup("Stat to add - ", statAddType);
        if (GUILayout.Button("Add stat"))
        {
            if (!upgradeView.stats.ContainsKey(statAddType)) upgradeView.stats.Add(statAddType, 0);
        }
        EditorGUILayout.EndHorizontal();

        Dictionary<StatType, float> statLoopThrough = new Dictionary<StatType, float>();
        statLoopThrough.AddRange(upgradeView.stats);

        foreach (var stat in statLoopThrough)
        {
            EditorGUILayout.BeginHorizontal();
            upgradeView.stats[stat.Key] = EditorGUILayout.FloatField(stat.Key.ToString(), upgradeView.stats[stat.Key]);
            if (GUILayout.Button("Remove stat"))
            {
                upgradeView.stats.Remove(stat.Key);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    GameObject CreateNewAttachable()
    {
        string dir = AttatchableDataPath + UpgradeName + "_Attatchables";
        var uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(dir + "/", $"{UpgradeName}" + "attatchable.prefab"));

        GameObject nullObject = Resources.Load<GameObject>("DefaultAttatchable");
        nullObject.transform.position = Vector3.zero;
        nullObject.transform.localScale = Vector3.one;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        PrefabUtility.SaveAsPrefabAsset(nullObject, uniqueAssetPath);

        if (AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(nullObject), uniqueAssetPath))
        {
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            return AssetDatabase.LoadAssetAtPath<GameObject>(uniqueAssetPath);
        }
        return null;
    }

    bool CheckExists()
    {
        if (File.Exists(UpgradeDataPath + UpgradeName + ".asset"))
        {
            return true;
        }
        return false;
    }

    void CreateObjectInAssets()
    {
        if (currentEditing)
        {
            currentEditing.Icon = texture_upgradeIcon;
            currentEditing.Description = UpgradeDescription;
        }

        UpgradeScriptableObjects anUpgrade = CreateInstance<UpgradeScriptableObjects>();

        anUpgrade.Icon = texture_upgradeIcon;
        anUpgrade.Description = UpgradeDescription;

        foreach (UpgradeView u in addedUpgradeTypes)
        {
            if (u.attatchObject != null)
            {
                u.attatchObject.transform.position /= 2;

                u.attatchObject.transform.position = new Vector3(u.attatchObject.transform.position.x, u.attatchObject.transform.position.y * -1, 0);

                anUpgrade.attachPrefabs.Add(u.attatchObject);
            }

            foreach (var v in u.stats)
            {
                switch (v.Key)
                {
                    case StatType.Damage:
                        anUpgrade.Damage = v.Value;
                        break;
                    case StatType.Speed:
                        anUpgrade.Speed = v.Value;
                        break;
                    case StatType.WeaponSize:
                        anUpgrade.WeaponSize = v.Value;
                        break;
                    case StatType.OrbitRadius:
                        anUpgrade.OrbitRadius = v.Value;
                        break;
                    case StatType.Health:
                        anUpgrade.Health = v.Value;
                        break;
                }
            }
        }

        AssetDatabase.CreateAsset(anUpgrade, UpgradeDataPath + UpgradeName + ".asset");
        ClearData();
        PopulateUpgradeManager();
    }

    void GetAllUpgrades()
    {
        string[] guids = AssetDatabase.FindAssets("t:UpgradeScriptableObjects", null); //file paths

        foreach (string guid in guids)
        {
            UpgradeScriptableObjects upgrade = (UpgradeScriptableObjects)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(UpgradeScriptableObjects));
            upgradesInAssets.Add(upgrade);
        }
    }

    void LoadUpgrade(UpgradeScriptableObjects anUpgrade)
    {
        currentEditing = anUpgrade;
        UpgradeName = anUpgrade.name;
        UpgradeDescription = anUpgrade.Description;
        texture_upgradeIcon = anUpgrade.Icon;


        addedUpgradeTypes = new List<UpgradeView>();

        if (anUpgrade.WeaponSize != 0 || anUpgrade.Health != 0 || anUpgrade.Speed != 0 || anUpgrade.Damage != 0 || (anUpgrade.OrbitRadius != 0))
        {
            Dictionary<StatType, float> someStats = new Dictionary<StatType, float>();

            if (anUpgrade.WeaponSize != 0) someStats.Add(StatType.WeaponSize, anUpgrade.WeaponSize);
            if (anUpgrade.Health != 0) someStats.Add(StatType.Health, anUpgrade.WeaponSize);
            if (anUpgrade.Speed != 0) someStats.Add(StatType.Speed, anUpgrade.WeaponSize);
            if (anUpgrade.Damage != 0) someStats.Add(StatType.Damage, anUpgrade.WeaponSize);
            if (anUpgrade.OrbitRadius != 0) someStats.Add(StatType.OrbitRadius, anUpgrade.WeaponSize);

            UpgradeView view = new UpgradeView(UpgradeType.AddStats, someStats, null);
            addedUpgradeTypes.Add(view);
        }

        if (anUpgrade.attachPrefabs.Count() > 0)
        {
            foreach (GameObject g in anUpgrade.attachPrefabs)
            {
                UpgradeView view = new UpgradeView();
                view.type = UpgradeType.AttatchToPlayer;
                view.attatchObject = g;
                addedUpgradeTypes.Add(view);
            }
        }
    }

    void PopulateUpgradeManager()
    {
        string[] guids = AssetDatabase.FindAssets("t:UpgradeScritbaleObjects", null); //check file paths
        List<UpgradeScriptableObjects> upgrades = new List<UpgradeScriptableObjects>();
        foreach (string guid in guids)
        {
            UpgradeScriptableObjects upgrade = (UpgradeScriptableObjects)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(UpgradeScriptableObjects));
            upgrades.Add(upgrade);
        }

        UpgradeContainerScriptableObjects m = Resources.Load<UpgradeContainerScriptableObjects>("Utils/MainUpgradeContainer"); //file paths
        m.Upgrades = new List<UpgradeScriptableObjects>();
        m.Upgrades.AddRange(upgrades);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        GetAllUpgrades();
    }

    private void ClearData()
    {
        AssetDatabase.Refresh();

        currentEditing = null;

        addedUpgradeTypes.Clear();
        upgradesInAssets.Clear();

        UpgradeDescription = "";

        UpgradeName = "";
    }
    private void OnEnable()
    {
        GetAllUpgrades();
        default_SpriteMaterial = Resources.Load<Material>("Utils/SpriteDefault"); //file paths
        texture_dot = Resources.Load<Texture2D>("Utils/Dot");
        texture_playerReference = Resources.Load<Texture2D>("Utils/red_character");
        addedUpgradeTypes = new List<UpgradeView>();

    }
    private void OnDisable()
    {
        ClearData();

        texture_upgradeIcon = null;
        texture_playerReference = null;
        texture_dot = null;
        default_SpriteMaterial = null;
    }
}
