using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(HexTile))]
[CanEditMultipleObjects]
public class HexTileEditor : Editor
{
    private static Type[] pieceTypes;
    private static Type[] PieceTypes
    {
        get
        {
            InitStaticFields();
            return pieceTypes;
        }
    }

    private static string[] pieceTypeNames;
    private static string[] PieceTypeNames
    {
        get
        {
            InitStaticFields();
            return pieceTypeNames;
        }
    }

    private static string[] tileTypeNames;
    private static string[] TileTypeNames
    {
        get
        {
            InitStaticFields();
            return pieceTypeNames;
        }
    }

    private int selectedPiece = 0;
    private int player = 1;

    public override void OnInspectorGUI()
    {
        UnityEngine.Object[] tiles = targets;

        
        EditorGUILayout.BeginToggleGroup("", false);
        DrawDefaultInspector();
        EditorGUILayout.EnumPopup("State", ((HexTile)tiles[0]).State);
        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Setup Type", EditorStyles.boldLabel);
        EditorGUILayout.EnumPopup("Type", ((HexTile)tiles[0]).Type);

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Setup Piece", EditorStyles.boldLabel);
        selectedPiece = EditorGUILayout.Popup("Piece:", selectedPiece, PieceTypeNames);
        player = EditorGUILayout.IntSlider("Player:", player, 1, 2);


        if (GUILayout.Button(selectedPiece == 0 ? "Clear Piece" : "Set Piece"))
        {
            foreach (HexTile tile in tiles)
            {
                switch (selectedPiece)
                {
                    case 0:
                        tile.ClearPiece();
                        break;
                    default:
                        tile.SetupPiece(player, PieceTypes[selectedPiece - 1]);
                        break;
                }
            }
        }


    }

    private static void InitStaticFields()
    {
        if (pieceTypes == null || pieceTypeNames == null)
        {
            pieceTypes = Assembly.GetAssembly(typeof(Piece)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Piece)))
                .ToArray();

            pieceTypeNames = new string[pieceTypes.Length + 1];
            pieceTypeNames[0] = "None";
            for (int i = 0; i < pieceTypes.Length; i++)
            {
                pieceTypeNames[i + 1] = pieceTypes[i].Name;
            }
        }
    }
}
