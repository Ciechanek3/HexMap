using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexProperties
{
    public readonly int xCoordinate;
    public readonly int yCoordinate;
    public readonly Color color;

    public HexProperties(int x, int y, Color c)
    {
        xCoordinate = x;
        yCoordinate = y;
        color = c;
    }
}
