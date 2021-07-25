/*****************************************************************************
// File Name :         ShopEditor.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
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
