using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public Material[] Materials;
    [SerializeField]
    public TileArray<HexTile> Tiles { get; private set; }
    [HideInInspector]
    public int width = 1;

    //Gubi referencję do planszy.
    public void Start()
    {
        HexTile[] tiles = GetComponentsInChildren<HexTile>();
        InitializeTileMatrix(
            tiles.Min(x => x.AxialX),
            tiles.Max(x => x.AxialX),
            tiles.Min(y => y.AxialY),
            tiles.Max(y => y.AxialY));

        foreach (HexTile t in tiles)
        {
            Tiles[t.AxialX, t.AxialY, t.Level] = t;
        }
    }

    #region Tiles

    private void InitializeTileMatrix(int minX, int maxX, int minY, int maxY)
    {
        Tiles = new TileArray<HexTile>(minX, maxX, minY, maxY);
    }

    #endregion

    #region GenerateMap
        
    public void GenerateMap(MapType type)
    {
        ClearMap();

        switch (type)
        {
            case MapType.Disc:
                GenerateDiscMap(width, HexTile.eLevel.Up);
                GenerateDiscMap(width, HexTile.eLevel.Down);
                break;
        }
    }

    private void ClearMap()
    {
        foreach (HexTile h in this.GetComponentsInChildren<HexTile>())
        {
            if (h.IsOcuppied) DestroyImmediate(h.OcuppiedBy.gameObject);
            DestroyImmediate(h.transform.gameObject);
        }
    }

    private void GenerateDiscMap(int rings, HexTile.eLevel level)
    {
        InitializeTileMatrix(-rings, rings, -rings, rings);

        HexTile h = HexTile.CreateTile(this.transform, 0, 0, level);
        h.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        Tiles[0, 0, level] = h;
        
        for (int r = 1; r <= rings; ++r)
        {

            for (int x = 0; x < r; ++x)
            {
                //North
                h = HexTile.CreateTile(this.transform, x, -r, level);
                Tiles[x, -r, level] = h;

                //NorthEast
                h = HexTile.CreateTile(this.transform, r, -r + x, level);
                Tiles[r, -r + x, level] = h;

                //SouthEast
                h = HexTile.CreateTile(this.transform, r - x, x, level);
                Tiles[r - x, x, level] = h;

                //South
                h = HexTile.CreateTile(this.transform, -x, r, level);
                Tiles[-x, r, level] = h;

                //SouthWest
                h = HexTile.CreateTile(this.transform, -r, r - x, level);
                Tiles[-r, r - x, level] = h;

                //NorthWest
                h = HexTile.CreateTile(this.transform, x - r, -x, level);
                Tiles[x - r, -x, level] = h;
            }
        }

        int[] xNeighbor = new int[6] { 0, 1, 1, 0, -1, -1 };
        int[] yNeighbor = new int[6] { -1, -1, 0, 1, 1, 0 };
        HexTile neighbor;

        for (int i = -rings; i <= rings; ++i)
        {
            for (int j = -rings; j <= rings; ++j)
            {
                h = Tiles[i, j, level];

                if (h != null)
                {
                    foreach (HexTile.eDirection d in Enum.GetValues(typeof(HexTile.eDirection)))
                    {
                        neighbor = Tiles[i + xNeighbor[(int)d], j + yNeighbor[(int)d], level];

                        h.SetNeighbour(neighbor, d);
                    }
                }
            }
        }
    }

    #endregion
}

public enum MapType
{
    Disc
    , Ring
}