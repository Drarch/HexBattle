using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class HexMoveMap
{
    public HexMoveMap(HexTile.eState state, HexMoveMapParametrs parameters, HexTile.Coordinate[] map)
    {
        State = state;
        Parameters = parameters;
        MoveMap = map;
    }

    public void SelectMap(HexTile root, int player)
    {
        foreach (HexTile.Coordinate c in MoveMap)
        {
            if (root.Map.Tiles[root.AxialX + c.coordinateX, root.AxialY + c.coordinateY, root.Level] != null)
            {
                HexTile tile = root.Map.Tiles[root.AxialX + c.coordinateX, root.AxialY + c.coordinateY, root.Level];

                if (Parameters.MustBeOccupied == tile.IsOcuppied && (Parameters.DifferentPlayers ? tile.OcuppiedBy.Player != player : true))
                {
                    tile.State = State;
                }
            }
        }
    }
}
