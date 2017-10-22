using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateWeapon  {

    [MenuItem("Assets/Create/WeaponObj")]
    public static void Create()
    {
        WeaponObj asset = ScriptableObject.CreateInstance<WeaponObj>();
        AssetDatabase.CreateAsset(asset, "Assets/NewWeaponObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
