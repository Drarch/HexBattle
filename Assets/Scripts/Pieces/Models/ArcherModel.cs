using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Archer : Piece
{
    protected override HexMoveMap GenerateMoveMap()
    {
        HexTile.eState state = HexTile.eState.Moveable;
        HexMoveMapParametrs parameters = HexMoveMapParametrs.GetMoveParameters();
        HexTile.Coordinate[] moveMap = HexTile.RingInRadiusCoordinates(0, 0, 0, 1);

        return new HexMoveMap(state, parameters, moveMap);
    }

    protected override HexMoveMap GenerateAttackMap()
    {
        HexTile.eState state = HexTile.eState.Atackable;
        HexMoveMapParametrs parameters = HexMoveMapParametrs.GetAttackParameters();
        HexTile.Coordinate[] moveMap = HexTile.RingInRadiusCoordinates(0, 0, 0, 2);

        return new HexMoveMap(state, parameters, moveMap);
    }

    protected override HexTileMap GenerateMovePattern()
    {
        HexMoveMapParametrs move = new HexMoveMapParametrs() { DifferentPlayers = false, JumpOver = false, MustBeOccupied = false };
        HexMoveMapParametrs attack = new HexMoveMapParametrs() { DifferentPlayers = true, JumpOver = false, MustBeOccupied = true };

        HexTileMap result = new HexTileMap(HexTile.eState.Moveable, move);

        result.AddTile(HexTile.eDirection.North);
        result.AddTile(HexTile.eDirection.NorthEast);
        result.AddTile(HexTile.eDirection.NorthWest);
        result.AddTile(HexTile.eDirection.South);
        result.AddTile(HexTile.eDirection.SouthEast);
        result.AddTile(HexTile.eDirection.SouthWest);
        
        result.North.AddTile(HexTile.eDirection.NorthEast, HexTile.eState.Atackable, attack);
        result.NorthEast.AddTile(HexTile.eDirection.SouthEast, HexTile.eState.Atackable, attack);
        result.SouthEast.AddTile(HexTile.eDirection.South, HexTile.eState.Atackable, attack);
        result.South.AddTile(HexTile.eDirection.SouthWest, HexTile.eState.Atackable, attack);
        result.SouthWest.AddTile(HexTile.eDirection.NorthWest, HexTile.eState.Atackable, attack);
        result.NorthWest.AddTile(HexTile.eDirection.North, HexTile.eState.Atackable, attack);
        
        result.North.AddTile(HexTile.eDirection.North, HexTile.eState.Atackable, attack);
        result.NorthEast.AddTile(HexTile.eDirection.NorthEast, HexTile.eState.Atackable, attack);
        result.SouthEast.AddTile(HexTile.eDirection.SouthEast, HexTile.eState.Atackable, attack);
        result.South.AddTile(HexTile.eDirection.South, HexTile.eState.Atackable, attack);
        result.SouthWest.AddTile(HexTile.eDirection.SouthWest, HexTile.eState.Atackable, attack);
        result.NorthWest.AddTile(HexTile.eDirection.NorthWest, HexTile.eState.Atackable, attack);

        return result;
    }
}