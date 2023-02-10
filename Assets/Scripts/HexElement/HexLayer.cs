using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexLayer
{
    public Hex[,] ActiveHexes;
    public HexProperties[,] HexProperties;

    public readonly int xSize;
    public readonly int zSize;
    public readonly int xIndex;
    public readonly int zIndex;
    public readonly int HexPropertiesIndex;

    public HexLayer(int x, int z, int xI, int zI, int cI)
    {
        xSize = x;
        zSize = z;
        ActiveHexes = new Hex[xSize, zSize];
        HexProperties = new HexProperties[xSize, zSize];
        xIndex = xI;
        zIndex = zI;
        HexPropertiesIndex = cI;
    }

    public void SetupProperties(int xIndex, int zIndex, int x, int y, Vector3 p)
    {
        HexProperties[xIndex, zIndex] = new HexProperties(x, y, p);
    }

    public void ChangePropertiesOfHex(int i, int j)
    {
        ActiveHexes[i, j].ChangeProperties(HexProperties[i, j].Position);
    }
}
