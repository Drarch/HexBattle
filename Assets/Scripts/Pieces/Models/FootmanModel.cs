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
        HexTile.Coordinate[] moveMap = HexTile.RingInRadiusCoordinates(0, 0, 0, 1);

        return new HexMoveMap(state, parameters, moveMap);
    }

    protected override HexMoveMap GenerateAttackMap()
    {
        HexTile.eState state = HexTile.eState.Atackable;
        HexMoveMapParametrs parameters = HexMoveMapParametrs.GetAttackParameters();
        HexTile.Coordinate[] moveMap = HexTile.RingInRadiusCoordinates(0, 0, 0, 1);

        return new HexMoveMap(state, parameters, moveMap);
    }
}