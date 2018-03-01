using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Assasin : Piece
{
    protected override HexMoveMap GenerateAttackMap()
    {
        throw new NotImplementedException();
    }

    protected override HexMoveMap GenerateMoveMap()
    {
        throw new NotImplementedException();
    }

    protected override HexTileMap GenerateMovePattern()
    {
        HexMoveMapParametrs ignore = new HexMoveMapParametrs() { JumpOver = true, MustBeOccupied = true };
        HexMoveMapParametrs move = new HexMoveMapParametrs() {  JumpOver = false, MustBeOccupied = false };
        HexMoveMapParametrs attack = new HexMoveMapParametrs() { JumpOver = false, MustBeOccupied = false };

        HexTileMap result = new HexTileMap(HexTile.eState.Moveable, ignore);

        result.AddTile(HexTile.eDirection.North, HexTile.eState.Moveable, null);
        result.AddTile(HexTile.eDirection.NorthEast, HexTile.eState.Moveable, null);
        result.AddTile(HexTile.eDirection.NorthWest, HexTile.eState.Moveable, null);
        result.AddTile(HexTile.eDirection.South, HexTile.eState.Moveable, null);
        result.AddTile(HexTile.eDirection.SouthEast, HexTile.eState.Moveable, null);
        result.AddTile(HexTile.eDirection.SouthWest, HexTile.eState.Moveable, null);
        
        result.North.AddTile(HexTile.eDirection.NorthEast, HexTile.eState.Moveable, move);
        result.NorthEast.AddTile(HexTile.eDirection.SouthEast, HexTile.eState.Moveable, move);
        result.SouthEast.AddTile(HexTile.eDirection.South, HexTile.eState.Moveable, move);
        result.South.AddTile(HexTile.eDirection.SouthWest, HexTile.eState.Moveable, move);
        result.SouthWest.AddTile(HexTile.eDirection.NorthWest, HexTile.eState.Moveable, move);
        result.NorthWest.AddTile(HexTile.eDirection.North, HexTile.eState.Moveable, move);
        
        result.North.AddTile(HexTile.eDirection.North, HexTile.eState.Atackable, attack);
        result.NorthEast.AddTile(HexTile.eDirection.NorthEast, HexTile.eState.Atackable, attack);
        result.SouthEast.AddTile(HexTile.eDirection.SouthEast, HexTile.eState.Atackable, attack);
        result.South.AddTile(HexTile.eDirection.South, HexTile.eState.Atackable, attack);
        result.SouthWest.AddTile(HexTile.eDirection.SouthWest, HexTile.eState.Atackable, attack);
        result.NorthWest.AddTile(HexTile.eDirection.NorthWest, HexTile.eState.Atackable, attack);

        return result;
    }
}