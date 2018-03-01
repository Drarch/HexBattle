using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract public partial class Footman : Piece
{
    protected override HexMoveMap GenerateMoveMap()
    {
        HexTile.eState state = HexTile.eState.Moveable;
        HexMoveMapParametrs parameters = HexMoveMapParametrs.GetMoveParameters();
        HexTile.Coordinate[] moveMap = HexTile.RingInRadiusCoordinates(0, 0, 1);

        return new HexMoveMap(state, parameters, moveMap);
    }

    protected override HexMoveMap GenerateAttackMap()
    {
        HexTile.eState state = HexTile.eState.Atackable;
        HexMoveMapParametrs parameters = HexMoveMapParametrs.GetAttackParameters();
        HexTile.Coordinate[] moveMap = HexTile.RingInRadiusCoordinates(0, 0, 1);

        return new HexMoveMap(state, parameters, moveMap);
    }

    protected override HexTileMap GenerateMovePattern()
    {
        Dictionary<HexTile.eState, HexMoveMapParametrs> states = new Dictionary<HexTile.eState, HexMoveMapParametrs>();

        states.Add(HexTile.eState.Moveable, new HexMoveMapParametrs() { DifferentPlayers = false, JumpOver = false, MustBeOccupied = false });
        states.Add(HexTile.eState.Atackable, new HexMoveMapParametrs() { DifferentPlayers = true, JumpOver = false, MustBeOccupied = true });

        HexTileMap result = new HexTileMap(states);

        result.AddTile(HexTile.eDirection.North);
        result.AddTile(HexTile.eDirection.NorthEast);
        result.AddTile(HexTile.eDirection.NorthWest);
        result.AddTile(HexTile.eDirection.South);
        result.AddTile(HexTile.eDirection.SouthEast);
        result.AddTile(HexTile.eDirection.SouthWest);

        return result;
    }
}