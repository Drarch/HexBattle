using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public partial class Piece
{
    public int HP { get; protected set; }
    public int MaxHP { get; protected set; }

    public BaseTile Tile
    {
        get; set;
    }

    public int Player;// { get; set; }

    private bool isAttacking;
    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }
        set
        {
            isAttacking = value;
            if(value) GetComponentInChildren<Animator>().SetTrigger("IsAttacking");
        }
    }
    public bool IsMoving { get; set; }
    public float MoveTime { get; set; }
    public Vector3 MoveFrom { get; set; }
    public Vector3 MoveTo { get; set; }

    //private HexTileMap moveMap; 
    //public HexTileMap MoveMap
    //{
    //    get
    //    {
    //        if(moveMap == null)
    //        {
    //            moveMap = this.GenerateMovePattern();
    //        }

    //        return moveMap;
    //    }
    //}

    private HexMoveMap moveMap;
    public HexMoveMap MoveMap
    {
        get
        {
            if (moveMap == null)
            {
                moveMap = this.GenerateMoveMap();
            }

            return moveMap;
        }
    }

    private HexMoveMap attackMap;
    public HexMoveMap AttackMap
    {
        get
        {
            if (attackMap == null)
            {
                attackMap = this.GenerateAttackMap();
            }

            return attackMap;
        }
    }
}