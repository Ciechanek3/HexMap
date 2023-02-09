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

    private void Start()
    {
        xOffset = 0;
        zOffset = 0;

        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    public void ChangeLayer(Transform transform)
    {
        if (hexGridGenerator.XGridLayerSize + initialX < transform.position.x)
        {
            xOffset++;
            initialX += hexGridGenerator.XGridLayerSize;
            hexGridGenerator.GetLayer(xOffset, 0);
        }

        if (initialX - hexGridGenerator.XGridLayerSize / 2 > transform.position.x)
        {
            Debug.Log("1");
            xOffset--;
            initialX -= hexGridGenerator.XGridLayerSize;
            hexGridGenerator.GetLayer(xOffset, 0);
        }
    }
}
