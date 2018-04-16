using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MazeDirection  {
	north,
    east,
    south,
    west
}

public static class MazeDirections
{
    public const int Count = 4;

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, Count);
        }
    }

    private static IntVector2[] vectors =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0)
    };
    private static MazeDirection[] opposites =
    {
        MazeDirection.south,
        MazeDirection.west,
        MazeDirection.north,
        MazeDirection.east
    };
    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,90f,0f),
        Quaternion.Euler(0f,180f,0f),
        Quaternion.Euler(0f,270f,0f)
    };
    public static Quaternion ToRotation(this MazeDirection dir)
    {
        return rotations[(int)dir];
    }
    public static MazeDirection GetOpposite(this MazeDirection dir)
    {
        return opposites[(int)dir];
    }
    public static IntVector2 ToIntVec2 (this MazeDirection dir)
    {
        return vectors[(int)dir];
    }
    public static MazeDirection GetNextClockwise(this MazeDirection direction)
    {
        return (MazeDirection)(((int)direction+1)%Count);
    }
    public static MazeDirection GetNextCounterclockwise(this MazeDirection direction)
    {
        return (MazeDirection)(((int)direction + Count - 1) % Count);
    }
}
