/*****************************************************************************
// File Name :         ShopEditor.cs
// Author :            Kyle Grenier
// Creation Date :     07/25/2021
//
// Brief Description : Custom inspector button to clear player save data.
*****************************************************************************/
#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Shop))]
public class ShopEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear Player Shop Data"))
        {
            SaveSystem.ClearData();
        }
    }
}

#endif