using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class TileArray<T>// : IEnumerator<T>//, IEnumerable<T>
{
    //private int position = 0;

    public int MinX { get; private set; }
    public int MaxX { get; private set; }
    public int MinY { get; private set; }
    public int MaxY { get; private set; }
    
    private T[,,] list { get; set; }

    public T this[int keyX, int keyY, HexTile.eLevel level]
    {
        get
        {
            if (keyX < MinX || keyX > MaxX
                || keyY < MinY || keyY > MaxY)
                return default(T);

            int levelIndex = level == HexTile.eLevel.Up ? 0 : 1;

            return list[keyX - MinX, keyY - MinY, levelIndex];
        }

        set
        {
            int levelIndex = level == HexTile.eLevel.Up ? 0 : 1;
            list[keyX - MinX, keyY - MinY, levelIndex] = value;
        }
    }

    public int Length { get { return list.Length; } }

    public TileArray(int minX, int maxX, int minY, int maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;

        list = new T[MaxX - MinX + 1, MaxY - MinY + 1, 2];
    }

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
