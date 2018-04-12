using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;
using System.Linq;
using System.Collections.Generic;

[SelectionBase]
public partial class HexTile : MonoBehaviour, INotifyPropertyChanged
{
    public void Start()
    {
        PropertyChanged += OnStateChanged;

        if(IsOcuppied)
        {
            OcuppiedBy.Tile = this;
        }
    }
    
    public void InitHexTile()
    {
        Neighbors = new HexTile[6];
        Type = eType.Basic;
    }

    public void SetCoordinate(int x, int y, eLevel level)
    {
        AxialX = x;
        AxialY = y;
        Level = level;

        //WorldX = (HexTile.HexWidth * 0.75f) * x;
        //WorldY = (HexTile.HexHeigth * y) - (HexTile.HexHeigth - ((HexTile.HexHeigth / 2.0f) * x)) + HexTile.HexHeigth;

        this.name = String.Format("{0}, {1}, {2}", AxialX, AxialY, level.ToString());
    }

    public void SetNeighbour(HexTile t, eDirection dirc)
    {
        Neighbors[(int)dirc] = t;
    }

    public HexTile GetNeighbour(eDirection dirc)
    {
        return Neighbors[(int)dirc];
    }

    public void SetStateChanged()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Material[] m = r.materials; 
        m[0] = Materials[(int)State];
        r.materials = m;
    }

    public void OnStateChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals(PropertyDictionary.StateProperty))
        {
            SetStateChanged();
        }
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public static HexTile CreateTile(Transform map, int x, int y, eLevel level)
    {
        float offsetX = (HexTile.HexWidth * 0.75f) * x;
        float offsetY = (HexTile.HexHeigth * y) - (HexTile.HexHeigth - ((HexTile.HexHeigth / 2.0f) * x)) + HexTile.HexHeigth;
        float offsetLevel = HexTile.HexThickness * (int)level;
        GameObject prefabHex = HexTile.GetPrefab();

        Quaternion rotation = level == eLevel.Up ? Quaternion.identity : Quaternion.AngleAxis(180.0f, Vector3.right);
        GameObject g = (GameObject)Instantiate(prefabHex, new Vector3(offsetX, offsetLevel, offsetY), rotation);

        g.transform.parent = map;
        HexTile h = g.GetComponent<HexTile>();
        h.InitHexTile();
        h.SetCoordinate(x, y, level);

        return h;
    }

    public static Coordinate[] RingInRadiusCoordinates(int baseX, int baseY, eLevel level, int radius)
    {
        List<Coordinate> result = new List<Coordinate>();

        if (radius > 0)
        {
            int maxX = baseX + radius, minX = baseX - radius,
                maxY = baseY + radius, minY = baseY - radius;

            for (int i = minY; i <= baseY; i++) { result.Add(new Coordinate(maxX, i, level)); }
            for (int i = baseY; i <= maxY; i++) {result.Add(new Coordinate(minX, i, level)); }
            for (int i = minX + 1; i <= baseX; i++) { result.Add(new Coordinate(i, maxY, level)); }
            for (int i = baseX; i < maxX; i++) { result.Add(new Coordinate(i, minY, level)); }
            for (int i = 1; i <= radius; i++)
            {
                result.Add(new Coordinate(baseX - i, minY + i, level));
                result.Add(new Coordinate(maxX - i, baseY + i, level));
            }
        }
        else if (radius == 0)
        {
            result.Add(new Coordinate(baseX, baseY, level));
        }

        return result.ToArray();
    }

    public Coordinate[] RingInRadiusCoordinates(int radius)
    {
        return RingInRadiusCoordinates(AxialX, AxialY, this.Level, radius);
    }

    public HexTile[] RingInRadius(int radius)
    {
        List<HexTile> result = new List<HexTile>();

        foreach(Coordinate c in RingInRadiusCoordinates(radius))
        {
            if(Map.Tiles[c.coordinateX, c.coordinateY, this.Level] != null)
            {
                result.Add(Map.Tiles[c.coordinateX, c.coordinateY, this.Level]);
            }
        }
        
        return result.ToArray();
    }

    public HexTile[] DiscInRadius(int radius)
    {
        List<HexTile> result = new List<HexTile>();

        if (radius >= 0)
        {
            for (int i = 0; i <= radius; i++)
            {
                result.AddRange(RingInRadius(i));
            }
        }

        return result.ToArray();
    }

    #region Pieces

    public void SetupPiece(int player, Type pieceType)
    {
        ClearPiece();

        GameObject pieces = GameObject.Find("Pieces");

        GameObject prefabPiece = Piece.GetPrefab(player, pieceType);
        GameObject piece = (GameObject)Instantiate(prefabPiece, this.transform.position, this.transform.rotation);

        if (pieces != null)
        {
            piece.transform.parent = pieces.transform;
        }
        
        this.OcuppiedBy = piece.GetComponent<Piece>();
        Piece p = piece.GetComponent<Piece>();

        if (p != null)
        {
            p.Tile = this;
            p.Player = player;
        }
    }

    public void ClearPiece()
    {
        if(this.IsOcuppied)
        {
            DestroyImmediate(this.OcuppiedBy.gameObject);
            this.OcuppiedBy = null;
        }
    }

    #endregion
}