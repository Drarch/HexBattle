using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexMap))]
public class HexMapEditor : Editor
{
    public MapType type;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Separator();

        HexMap map = (HexMap)target;

        type = (MapType)EditorGUILayout.EnumPopup("Map grid type:", type);

        switch (type)
        {
            case MapType.Disc:
                map.width = EditorGUILayout.IntSlider(new GUIContent("Radius:"), map.width, 0, 16);
                break;
        }

        if (GUILayout.Button("Generate"))
        {
            map.GenerateMap(type);
        }
    }
}
