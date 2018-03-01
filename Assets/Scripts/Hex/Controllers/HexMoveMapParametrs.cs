using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class HexMoveMapParametrs
{
    public static HexMoveMapParametrs GetMoveParameters()
    {
        return new HexMoveMapParametrs() { DifferentPlayers = false, JumpOver = false, MustBeOccupied = false };
    }

    public static HexMoveMapParametrs GetAttackParameters()
    {
        return new HexMoveMapParametrs() { DifferentPlayers = true, JumpOver = false, MustBeOccupied = true };
    }
}
