using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public partial class Assasin
{
    public Assasin() : base()
    {
        MaxHP = 1;
        HP = MaxHP;
    }

    public override void Attack(Piece target)
    {
        Piece[] team = target.Tile.Neighbors.Where(x => x.IsOcuppied && x.OcuppiedBy.Player == this.Player).Select(x => x.OcuppiedBy).ToArray();

        foreach (Piece p in team)
        {
            p.transform.LookAt(target.transform);
            p.IsAttacking = true;
            target.LoseHP();
        }
    }
}
