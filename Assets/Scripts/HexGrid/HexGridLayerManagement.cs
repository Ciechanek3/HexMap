using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayerManagement : MonoBehaviour
{
    [SerializeField]
    private HexGridGenerator hexGridGenerator;

    private float initialX;
    private float initialZ;

    private int xOffset;
    private int zOffset;

    public void SetupInitialValues(Transform transform)
    {
        xOffset = 0;
        zOffset = 0;

        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    public void ChangeLayerHorizontally(Transform transform)
    {
        if (hexGridGenerator.XGridLayerSize + initialX < transform.position.x)
        {
            xOffset++;
            initialX += hexGridGenerator.XGridLayerSize;
            hexGridGenerator.GetLayer(xOffset, zOffset);
        }

        if (initialX - hexGridGenerator.XGridLayerSize / 2 > transform.position.x)
        {
            xOffset--;
            initialX -= hexGridGenerator.XGridLayerSize;
            hexGridGenerator.GetLayer(xOffset, zOffset);
        }
    }

    public void ChangeLayerVertically(Transform transform)
    {
        if (hexGridGenerator.ZGridLayerSize + initialZ < transform.position.z)
        {
            zOffset++;
            initialZ += hexGridGenerator.ZGridLayerSize;
            hexGridGenerator.GetLayer(xOffset, zOffset);
        }

        if (initialZ - hexGridGenerator.ZGridLayerSize / 2 > transform.position.z)
        {
            zOffset--;
            initialZ -= hexGridGenerator.ZGridLayerSize;
            hexGridGenerator.GetLayer(xOffset, zOffset);
        }
    }
}
