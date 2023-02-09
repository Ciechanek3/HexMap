using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexProperties
{
    public readonly int xCoordinate;
    public readonly int yCoordinate;
    public readonly Vector3 Position;

    public HexProperties(int x, int y, Vector3 p)
    {
        xCoordinate = x;
        yCoordinate = y;
        Position = p;
    }
}
