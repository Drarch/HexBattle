using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public partial class Archer
{
    public Archer() : base()
    {
        MaxHP = 2;
        HP = MaxHP;
    }

    public override void Attack(Piece target)
    {
        transform.LookAt(target.transform);
        IsAttacking = true;
        target.LoseHP();
    }
}
