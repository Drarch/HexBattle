using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class HexTile
{
    //public double CenterX { get; set; }
    //public double CenterY { get; set; }
    public struct Coordinate
    {
        public int coordinateX;
        public int coordinateY;

        public Coordinate(int x, int y)
        {
            coordinateX = x;
            coordinateY = y;
        }
    };

    public HexMap Map
    {
        get { return this.GetComponentInParent<HexMap>(); }
    }

    private Material[] Materials
    {
        get
        {
            return Map.Materials;
        }
    }

    public static float hexRadius = 1.0f;

    public static float HexRadius
    {
        get { return hexRadius; }
        set { hexRadius = value; }
    }
    public static float HexWidth { get { return HexRadius * 2.0f; } }
    public static float HexHeigth { get { return ((float)System.Math.Sqrt(3.0f) / 2.0f) * HexWidth; } }

    public static GameObject GetPrefab()
    {
        return Resources.Load("Prefabs/HexUp", typeof(GameObject)) as GameObject;
    }

    public enum eDirection
    {
        North = 0
        , NorthEast
        , SouthEast
        , South
        , SouthWest
        , NorthWest
    }

    public enum eState
    {
        NotSelected = 0
        , Selected
        , Moveable
        , Atackable
    }

    public static class PropertyDictionary
    {
        public const string StateProperty = "State";
    }

    //private GameObject Grid { get; set; }

    public HexTile[] Neighbors;// { get; private set; }

    public Piece OcuppiedBy;// { get; set; }
    public bool IsOcuppied { get { return OcuppiedBy != null; } }

    public int AxialX;// { get; private set; }
    public int AxialY;// { get; private set; }

    public int CubeX { get { return AxialX; } }
    public int CubeY { get { return AxialX - AxialY; } }
    public int CubeZ { get { return AxialY; } }

    public float WorldX;// { get; private set; }
    public float WorldY;// { get; private set; }

    private eState state = eState.NotSelected;
    public eState State
    {
        get { return state; }
        set
        {
            if (value != state)
            {
                state = value;
                OnPropertyChanged(PropertyDictionary.StateProperty);
            }
        }
    }
}
