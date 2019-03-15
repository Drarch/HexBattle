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
                map.Width = EditorGUILayout.IntSlider(new GUIContent("Radius:"), map.Width, 0, 16);
                break;
            case MapType.Ring:
                map.Width = EditorGUILayout.IntSlider(new GUIContent("Radius:"), map.Width, 0, 16);
                map.RingMin = EditorGUILayout.IntSlider(new GUIContent("Hole Radius:"), map.RingMin, 0, 16);
                break;
        }

        if (GUILayout.Button("Generate"))
        {
            map.GenerateMap(type);
        }
    }
}
