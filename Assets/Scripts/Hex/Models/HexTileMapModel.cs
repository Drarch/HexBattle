using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class HexTileMap
{
    public Dictionary<HexTile.eDirection, HexTileMap> Map { get; private set; }
    public Dictionary<HexTile.eState, HexMoveMapParametrs> States { get; private set; }

    public HexTileMap North { get { return Map[HexTile.eDirection.North]; } }
    public HexTileMap NorthEast { get { return Map[HexTile.eDirection.NorthEast]; } }
    public HexTileMap NorthWest { get { return Map[HexTile.eDirection.NorthWest]; } }
    public HexTileMap South { get { return Map[HexTile.eDirection.South]; } }
    public HexTileMap SouthEast { get { return Map[HexTile.eDirection.SouthEast]; } }
    public HexTileMap SouthWest { get { return Map[HexTile.eDirection.SouthWest]; } }
}