using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public partial class HexMoveMapParametrs
{
    public bool DifferentPlayers { get; set; } //For heal mechanic
    public bool JumpOver { get; set; } //For Jumping mechanic
    public bool MustBeOccupied { get; set; } //For base movement
}
