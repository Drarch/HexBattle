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
    }

    public void SetCoordinate(int x, int y)
    {
        AxialX = x;
        AxialY = y;

        WorldX = (HexTile.HexWidth * 0.75f) * x;
        WorldY = (HexTile.HexHeigth * y) - (HexTile.HexHeigth - ((HexTile.HexHeigth / 2.0f) * x)) + HexTile.HexHeigth;

        this.name = String.Format("{0}, {1}", AxialX, AxialY);
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
        r.sharedMaterial = Materials[(int)State];
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

    public static HexTile CreateTile(Transform map, int x, int y)
    {
        float offsetX = (HexTile.HexWidth * 0.75f) * x;
        float offsetY = (HexTile.HexHeigth * y) - (HexTile.HexHeigth - ((HexTile.HexHeigth / 2.0f) * x)) + HexTile.HexHeigth;

        GameObject prefabHex = HexTile.GetPrefab();
        //h.transform.parent = this.transform;

        GameObject g = (GameObject)Instantiate(prefabHex, new Vector3(offsetX, 0, offsetY), Quaternion.identity);

        g.transform.parent = map;
        HexTile h = g.GetComponent<HexTile>();
        h.InitHexTile();
        h.SetCoordinate(x, y);

        return h;
    }

    public static Coordinate[] RingInRadiusCoordinates(int baseX, int baseY, int radius)
    {
        List<Coordinate> result = new List<Coordinate>();

        if (radius > 0)
        {
            int maxX = baseX + radius, minX = baseX - radius,
                maxY = baseY + radius, minY = baseY - radius;

            for (int i = minY; i <= baseY; i++) { result.Add(new Coordinate(maxX, i)); }
            for (int i = baseY; i <= maxY; i++) {result.Add(new Coordinate(minX, i)); }
            for (int i = minX + 1; i <= baseX; i++) { result.Add(new Coordinate(i, maxY)); }
            for (int i = baseX; i < maxX; i++) { result.Add(new Coordinate(i, minY)); }
            for (int i = 1; i <= radius; i++)
            {
                result.Add(new Coordinate(baseX - i, minY + i));
                result.Add(new Coordinate(maxX - i, baseY + i));
            }
        }
        else if (radius == 0)
        {
            result.Add(new Coordinate(baseX, baseY));
        }

        return result.ToArray();
    }

    public Coordinate[] RingInRadiusCoordinates(int radius)
    {
        return RingInRadiusCoordinates(AxialX, AxialY, radius);
    }

    public HexTile[] RingInRadius(int radius)
    {
        List<HexTile> result = new List<HexTile>();

        foreach(Coordinate c in RingInRadiusCoordinates(radius))
        {
            if(Map.Tiles[c.coordinateX, c.coordinateY] != null)
            {
                result.Add(Map.Tiles[c.coordinateX, c.coordinateY]);
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

    public void SetupPiece(int player, Type piece)
    {
        ClearPiece();

        GameObject pieces = GameObject.Find("Pieces");

        GameObject prefabPiece = Piece.GetPrefab(player, piece);
        GameObject g = (GameObject)Instantiate(prefabPiece, new Vector3(this.WorldX, 0, this.WorldY), Quaternion.identity);

        if (pieces != null)
        {
            g.transform.parent = pieces.transform;
        }

        this.OcuppiedBy = g.GetComponent<Piece>();
        Piece p = g.GetComponent<Piece>();

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