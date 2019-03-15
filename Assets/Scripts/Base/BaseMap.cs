using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMap<T> : MonoBehaviour
    where T : BaseTile
{
    public Material[] Materials;
    [SerializeField]
    public TileArray<T> Tiles { get; protected set; }
    [HideInInspector]
    public int Width { get; set; } = 1;

    protected abstract void InitializeTileMatrix(int minX, int maxX, int minY, int maxY);

    public abstract void GenerateMap(MapType type);

    protected void ClearMap()
    {
        foreach (T h in this.GetComponentsInChildren<T>())
        {
            if (h.IsOcuppied) DestroyImmediate(h.OcuppiedBy.gameObject);
            DestroyImmediate(h.transform.gameObject);
        }
    }
}
