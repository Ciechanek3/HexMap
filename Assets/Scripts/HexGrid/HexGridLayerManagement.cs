using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridLayerManagement : MonoBehaviour
{
    [SerializeField]
    private HexGridGenerator hexGridGenerator;
    [SerializeField]
    private CameraMovement cameraMovement;

    public int xGridLayer;
    public int zGridLayer;

    public List<HexLayer> HexLayers = new List<HexLayer>();

    private float initialX;
    private float initialZ;

    private int xOffset;
    private int zOffset;

    public float XGridLayerSize => xGridLayer * hexGridGenerator.xCellSize;
    public float ZGridLayerSize => zGridLayer * hexGridGenerator.xCellSize;

    private void Start()
    {
        int layerIndex = 0;
        CreateStartingLayer(layerIndex, 0, 0);
        layerIndex++;
        CreateStartingLayer(layerIndex, 1, 0);
        layerIndex++;
        CreateStartingLayer(layerIndex, 0, 1);
        layerIndex++;
        CreateStartingLayer(layerIndex, 1, 1);
        layerIndex++;
        CreateStartingLayer(layerIndex, 0, 2);
        layerIndex++;
        CreateStartingLayer(layerIndex, 2, 0);
        layerIndex++;
        CreateStartingLayer(layerIndex, 2, 1);
        layerIndex++;
        CreateStartingLayer(layerIndex, 1, 2);
        layerIndex++;
        CreateStartingLayer(layerIndex, 2, 2);

    }

    private void CreateStartingLayer(int layerIndex, int xOffset, int zOffset)
    {
        HexLayer startingLayer = new HexLayer(xGridLayer, zGridLayer, xOffset, zOffset, layerIndex);
        HexLayers.Add(startingLayer);
        hexGridGenerator.CreateElements(startingLayer, true);
    }

    public void SetupInitialValues(Transform transform)
    {
        xOffset = 0;
        zOffset = 0;

        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    public void ChangeLayerHorizontally(Transform transform)
    {
        float xCameraMovement = cameraMovement.inputReader.look.x;
        if (xCameraMovement > 0.5f)
        {
            if (initialX + XGridLayerSize / 6 < transform.position.x)
            {
                xOffset++;
                initialX += XGridLayerSize;
                hexGridGenerator.GetLayer(xOffset, zOffset);
            }
        }

        if (xCameraMovement < -0.5f)
        {
            if (initialX - XGridLayerSize / 6 > transform.position.x)
            {
                xOffset--;
                initialX -= XGridLayerSize;
                hexGridGenerator.GetLayer(xOffset, zOffset);
            }
        }
    }

    public void ChangeLayerVertically(Transform transform)
    {
        float zCameraMovement = cameraMovement.inputReader.look.y;
        if (zCameraMovement > 0.5f)
        {
            if (ZGridLayerSize / 6 + initialZ < transform.position.z)
            {
                zOffset++;
                initialZ += ZGridLayerSize;
                hexGridGenerator.GetLayer(xOffset, zOffset);
            }
        }

        if (zCameraMovement < -0.5f)
        {
            if (initialZ - ZGridLayerSize / 6 > transform.position.z)
            {
                zOffset--;
                initialZ -= ZGridLayerSize;
                hexGridGenerator.GetLayer(xOffset, zOffset);
            }
        }   
    }
}
