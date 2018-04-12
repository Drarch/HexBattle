using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class TileArray<T> : IEnumerable<T>
{
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
    
    public IEnumerator<T> GetEnumerator()
    {
        return new TilesEnum<T>(list, MaxX - MinX + 1, MaxY - MinY + 1, 2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator<T>)GetEnumerator();
    }
}

public class TilesEnum<T> : IEnumerator<T>
{
    private int positionX = -1;
    private int positionY = 0;
    private int positionLevel = 0;

    private int lengthX;
    private int lengthY;
    private int lengthLevel;

    private T[,,] list { get; set; }

    public TilesEnum(T[,,] _list, int _lengthX, int _lengthY, int _lengthLevel)
    {
        list = _list;
        lengthX = _lengthX;
        lengthY = _lengthY;
        lengthLevel = _lengthLevel;
    }

    public T Current
    {
        get
        {
            try
            {
                return list[positionX, positionY, positionLevel];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public bool MoveNext()
    {
        //Avoids going beyond the end of the collection.
        if (++positionX >= lengthX)
        {
            if (++positionY >= lengthY)
            {
                if (++positionLevel >= lengthLevel)
                {
                    return false;
                }

                positionY = 0;
            }

            positionX = 0;
        }

        return true;
    }

    public void Reset()
    {
        positionX = -1;
        positionY = 0;
        positionLevel = 0;
    }

    public void Dispose()
    {

    }
}
