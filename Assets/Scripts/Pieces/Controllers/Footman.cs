using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public partial class Footman
{
    public override void Attack(Piece target)
    {
        Piece[] team = target.Tile.Neighbors.Where(x => x.IsOcuppied && x.OcuppiedBy.Player == this.Player && x.OcuppiedBy is Footman).Select(x => x.OcuppiedBy).ToArray();

        foreach (Piece p in team)
        {
            p.transform.LookAt(target.transform);
            p.IsAttacking = true;
            target.LoseHP();
        }
    }
}
