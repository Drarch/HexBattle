using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class HexMoveMap
{
    HexTile.Coordinate[] MoveMap { get; set; }
    HexMoveMapParametrs Parameters { get; set; }
    HexTile.eState State { get; set; }
}
