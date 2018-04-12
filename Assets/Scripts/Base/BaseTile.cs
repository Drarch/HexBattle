using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract partial class BaseTile : MonoBehaviour, INotifyPropertyChanged
{
    public void Start()
    {
        PropertyChanged += OnStateChanged;

        if (IsOcuppied)
        {
            OcuppiedBy.Tile = this;
        }
    }

    #region INotifyPropertyChanged

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
            handler(this, e);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    public void OnStateChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals(PropertyDictionary.StateProperty))
        {
            SetStateChanged();
        }
    }

    public void SetStateChanged()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Material[] m = r.materials;
        m[0] = Materials[(int)State];
        r.materials = m;
    }
    #endregion

    #region Pieces

    public void SetupPiece(int player, Type pieceType)
    {
        ClearPiece();

        GameObject pieces = GameObject.Find("Pieces");

        GameObject prefabPiece = Piece.GetPrefab(player, pieceType);
        GameObject piece = (GameObject)Instantiate(prefabPiece, this.transform.position, this.transform.rotation);

        if (pieces != null)
        {
            piece.transform.parent = pieces.transform;
        }

        this.OcuppiedBy = piece.GetComponent<Piece>();
        Piece p = piece.GetComponent<Piece>();

        if (p != null)
        {
            p.Tile = this;
            p.Player = player;
        }
    }

    public void ClearPiece()
    {
        if (this.IsOcuppied)
        {
            DestroyImmediate(this.OcuppiedBy.gameObject);
            this.OcuppiedBy = null;
        }
    }

    #endregion
}
