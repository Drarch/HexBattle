using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : BaseMap<HexTile>
{
    public int RingMin { get; set; }

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

    override protected void InitializeTileMatrix(int minX, int maxX, int minY, int maxY)
    {
        if (Tiles == null || Tiles.MinX != minX)
        {
            Tiles = new TileArray<HexTile>(minX, maxX, minY, maxY);
        }
    }

    #endregion

    #region GenerateMap
        
    override public void GenerateMap(MapType type)
    {
        ClearMap();

        switch (type)
        {
            case MapType.Disc:
                GenerateDiscMap(Width, HexTile.eLevel.Up);
                GenerateDiscMap(Width, HexTile.eLevel.Down);
                SetDiscMapTilesNeighbourhood();
                break;
            case MapType.Ring:
                GenerateRingMap(RingMin, Width, HexTile.eLevel.Up);
                GenerateRingMap(RingMin, Width, HexTile.eLevel.Down);
                SetDiscMapTilesNeighbourhood();
                break;
        }
    }

    private void SetDiscMapTilesNeighbourhood()
    {
        int[] xNeighbor = new int[6] { 0, 1, 1, 0, -1, -1 };
        int[] yNeighbor = new int[6] { -1, -1, 0, 1, 1, 0 };
        HexTile neighbor;

        foreach (HexTile h in Tiles)
        {
            if (h != null)
            {
                foreach (HexTile.eDirection d in Enum.GetValues(typeof(HexTile.eDirection)))
                {
                    neighbor = Tiles[h.AxialX + xNeighbor[(int)d], h.AxialY + yNeighbor[(int)d], h.Level];

                    h.SetNeighbour(neighbor, d);
                }

                HexTile.eLevel otherSide = h.Level == HexTile.eLevel.Up ? HexTile.eLevel.Down : HexTile.eLevel.Up;

                if (Tiles[h.AxialX, h.AxialY, otherSide] != null)
                {
                    h.OtherSide = Tiles[h.AxialX, h.AxialY, otherSide];
                }
            }
        }
    }

    #region Disc Map

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
    }

    #endregion

    #region Ring Map

    private void GenerateRingMap(int ringMin, int ringMax, HexTile.eLevel level)
    {
        InitializeTileMatrix(-ringMax, ringMax, -ringMax, ringMax);

        HexTile h;

        //HexTile h = HexTile.CreateTile(this.transform, 0, 0, level);
        //h.GetComponentInChildren<Renderer>().sharedMaterial.color = Color.white;
        //Tiles[0, 0, level] = h;

        for (int r = ringMin; r <= ringMax; ++r)
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
    }

    #endregion

    #endregion
}

public enum MapType
{
    Disc
    , Ring
}