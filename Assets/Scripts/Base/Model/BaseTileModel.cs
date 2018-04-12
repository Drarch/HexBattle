using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract partial class BaseTile
{
    #region Enums
    public enum eType
    {
        Basic = 0,
        Portal
    }

    public enum eState
    {
        NotSelected = 0,
        Selected,
        Moveable,
        Atackable
    }

    public enum eLevel
    {
        Up = 1,
        Down = -1
    }
    #endregion

    #region INotifyPropertyChanged
    public static class PropertyDictionary
    {
        public const string StateProperty = "State";
    }

    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    public static float ModelWidth { get { return float.NaN; } }
    public static float ModelHeigth { get { return float.NaN; } }
    public static float ModelThickness { get { return float.NaN; } }

    //TODO: Rethink this field
    public abstract BaseMap<HexTile> Map { get; }
    //{
    //    get { return this.GetComponentInParent<BaseMap<BaseTile>>(); }
    //}

    #region Model Fields

    public struct Coordinate
    {
        public int coordinateX;
        public int coordinateY;
        public eLevel level;

        public Coordinate(int x, int y, eLevel l)
        {
            coordinateX = x;
            coordinateY = y;
            level = l;
        }
    };

    public static GameObject GetPrefab()
    {
        throw new System.NotSupportedException("Remeber to crete new method in child class.");
    }

    protected Material[] Materials
    {
        get
        {
            return Map.Materials;
        }
    }

    #endregion

    public BaseTile[] Neighbors;
    public BaseTile OtherSide;

    public Piece OcuppiedBy;
    public bool IsOcuppied { get { return OcuppiedBy != null; } }

    public int AxialX;
    public int AxialY;

    public eLevel Level;

    public eType Type;

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
