using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class HexTile
{
    #region Enums
    public enum eDirection
    {
        North = 0,
        NorthEast,
        SouthEast,
        South,
        SouthWest,
        NorthWest
    }
    #endregion

    #region Model Fields
    public static float hexRadius = 1.0f;

    public static float HexRadius
    {
        get { return hexRadius; }
        set { hexRadius = value; }
    }

    public new static float ModelWidth { get { return HexRadius * 2.0f; } }
    public new static float ModelHeigth { get { return ((float)System.Math.Sqrt(3.0f) / 2.0f) * ModelWidth; } }
    public new static float ModelThickness { get { return 0.2f; } }

    public new static GameObject GetPrefab()
    {
        return Resources.Load("Prefabs/HexTile", typeof(GameObject)) as GameObject;
    }
    #endregion

    public override BaseMap<HexTile> Map
    {
        get { return this.GetComponentInParent<HexMap>(); }
    }

    public int CubeX { get { return AxialX; } }
    public int CubeY { get { return AxialX - AxialY; } }
    public int CubeZ { get { return AxialY; } }
}
