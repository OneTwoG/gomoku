using UnityEngine;

public enum ChessType
{
    None = 0,
    Black = 1,
    White = 2,
}



public class BoardModel {
    public const int WinChessCount = 5;

    ChessType[,] _data = new ChessType[Board.CrossCount, Board.CrossCount];

    public ChessType Get(int x, int y)
    {

        if(x < 0 || x >= Board.CrossCount)
        {
            return ChessType.None;
        }

        if(y < 0 || y >= Board.CrossCount)
        {
            return ChessType.None;
        }

        return _data[x,y];
    }

    public bool Set(int x, int y, ChessType type)
    {
        if(x < 0 || x >= Board.CrossCount)
        {
            return false;
        }

        if(y < 0 || y >= Board.CrossCount)
        {
            return false;
        }

        _data[x, y] = type;
        return true;
    }

    /// <summary>
    /// 检查垂直方向的连接情况
    /// </summary>
    /// <param name="px"></param>
    /// <param name="py"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    int CheckVerticalLink(int px, int py, ChessType type)
    {
        //算上自己
        int linkCount = 1;

        //朝上
        for(int y = py +1; y<= Board.CrossCount; y++)
        {
            if(Get(px,y) == type)
            {
                linkCount++;

                if(linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        //朝下
        for(int y = py - 1; y >= 0; y--)
        {
            if(Get(px,y) == type)
            {
                linkCount++;

                if(linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }
        return linkCount;
    }

    // 检查水平方向连接情况
    int CheckHorizentalLink(int px, int py, ChessType type)
    {
        int linkCount = 1;

        // 朝右+
        for (int x = px + 1; x < Board.CrossCount; x++)
        {
            if (Get(x, py) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }


        // 朝左
        for (int x = px - 1; x >= 0; x--)
        {
            if (Get(x, py) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        return linkCount;
    }


    //检查斜边的情况
    int CheckBiasLink(int px, int py, ChessType type)
    {
        int ret = 0;
        int linkCount = 1;

        //左下
        for(int x = px - 1, y = py -1; x >= 0 && y >= 0; x--,y--)
        {
            if(Get(x,y) == type)
            {
                linkCount++;

                if(linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        //右上
        for (int x = px + 1, y = py + 1; x < Board.CrossCount && y < Board.CrossCount; x++, y++)
        {
            if(Get(x,y) == type)
            {
                linkCount++;

                if(linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }

        ret = linkCount;
        linkCount = 1;

        // 左上
        for (int x = px - 1, y = py + 1; x >= 0 && y < Board.CrossCount; x--, y++)
        {
            if (Get(x, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }


        // 右下
        for (int x = px + 1, y = py - 1; x < Board.CrossCount && y >= 0; x++, y--)
        {
            if (Get(x, y) == type)
            {
                linkCount++;

                if (linkCount >= WinChessCount)
                {
                    return linkCount;
                }
            }
            else
            {
                break;
            }
        }
        return Mathf.Max(ret, linkCount);
    }

    // 检查给定点周边的最大连接情况
    public int CheckLink(int px, int py, ChessType type)
    {
        int linkCount = 0;

        linkCount = Mathf.Max(CheckHorizentalLink(px, py, type), linkCount);
        linkCount = Mathf.Max(CheckVerticalLink(px, py, type), linkCount);
        linkCount = Mathf.Max(CheckBiasLink(px, py, type), linkCount);

        return linkCount;
    }

}
