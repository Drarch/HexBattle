using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class HexTileMap
{
    private HexTileMap()
    {
        Map = new Dictionary<HexTile.eDirection, HexTileMap>();
        States = new Dictionary<HexTile.eState, HexMoveMapParametrs>();
    }

    public HexTileMap(HexTile.eState status, HexMoveMapParametrs parametrs)
        : this()
    {
        States.Add(status, parametrs);
    }

    public HexTileMap(Dictionary<HexTile.eState, HexMoveMapParametrs> states)
        : this()
    {
        if(states != null)
            States = states;
    }

    public void AddTile(HexTile.eDirection direction)
    {
        HexTileMap hex = new HexTileMap(States);
        Map.Add(direction, hex);
    }

    public void AddTile(HexTile.eDirection direction, HexTile.eState status, HexMoveMapParametrs parametrs)
    {
        HexTileMap hex = new HexTileMap(status, parametrs);
        Map.Add(direction, hex);
    }

    public void SelectMap(HexTile root, int player)
    {
        foreach(KeyValuePair<HexTile.eDirection, HexTileMap> h in Map)
        {
            SelectMap((HexTile)root.Neighbors[(int)h.Key], h.Value, player);
        }
    }

    private void SelectMap(HexTile tile, HexTileMap map, int player)
    {
        if (tile != null)
        {
            foreach (KeyValuePair<HexTile.eDirection, HexTileMap> h in map.Map)
            {
                SelectMap((HexTile)tile.Neighbors[(int)h.Key], h.Value, player);
            }

            HexTile.eState result;

            var state = map.States
                .Where(x => x.Value.MustBeOccupied == tile.IsOcuppied)
                .Where(x => x.Value.DifferentPlayers ? tile.OcuppiedBy.Player != player : true);
            
            foreach (KeyValuePair<HexTile.eState, HexMoveMapParametrs> s in state)
            {
                result = s.Key;

                tile.State = result;
            }
        }
    }
}
