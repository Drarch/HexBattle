using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[SelectionBase]
public abstract partial class Piece : MonoBehaviour
{
    protected abstract HexMoveMap GenerateMoveMap();
    protected abstract HexMoveMap GenerateAttackMap();
    
    public abstract void Attack(Piece target);
    public void LoseHP()
    {
        if(--HP <= 0)
        {
            GetComponentInChildren<Animator>().SetTrigger("IsDying");
        }
    }

    public static GameObject GetPrefab(int player, Type piece)
    {
        string prefabName = @"Prefabs/Pieces/" + piece.Name + player.ToString();
        return Resources.Load(prefabName, typeof(GameObject)) as GameObject; ;
    }

    public void SelectMap(BaseTile root)
    {
        MoveMap.SelectMap((HexTile)root, Player);
        AttackMap.SelectMap((HexTile)root, Player);
    }
}
