using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    [SerializeField]
    private List<HexWithSpawnRatio> hexes;
    [Header("Sizes")]
    [SerializeField]
    private float xCellSize;
    [SerializeField]
    private float zCellSize;
    [SerializeField]
    private int xGridSize;
    [SerializeField]
    private int zGridSize;
    [SerializeField]
    private int xGridLayer;
    [SerializeField]
    private int zGridLayer;

    public float XGridLayerSize => xGridLayer * xCellSize;
    public float ZGridLayerSize => zGridLayer * xCellSize;
    

    public const float HEX_OFFSET = .5f;

    private List<Hex> _hexesToCreate = new List<Hex>();
    private Hex[,] _activeLayer = new Hex[20, 20];

    public float GridLength => xGridSize * xCellSize;
    public float GridHeight => zGridSize * zCellSize;

    private List<HexLayer> _hexLayers = new List<HexLayer>();
    private List<Coordinates> _availableCoordinates = new List<Coordinates>();
    private List<Coordinates> _startingCoordinates = new List<Coordinates>();

    private class HexLayer
    {
        public Hex[,] ActiveHexes;

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
            xIndex = xI;
            zIndex = zI;
            HexPropertiesIndex = cI;
        }
    }

    private class Coordinates
    {
        public readonly int X;
        public readonly int Z;

        public Coordinates(int x, int z)
        {
            X = x;
            Z = z;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < xGridLayer; i++)
        {
            for (int j = 0; j < zGridLayer; j++)
            {
                Coordinates coordinates = new Coordinates(i, j);
                _availableCoordinates.Add(coordinates);
            }
        }
        _startingCoordinates.AddRange(_availableCoordinates);
        HexLayer startingLayer = new HexLayer(xGridLayer, zGridLayer, 0, 0, 0);
        _hexLayers.Add(startingLayer);

        CreateListOfElements();
        CreateElements(startingLayer, true);
    }

    public void GetLayer(int xOffset, int zOffset)
    {
        for (int i = 0; i < _hexLayers.Count; i++)
        {
            if (_hexLayers[i].xIndex == xOffset && _hexLayers[i].zIndex == zOffset)
            {
                Debug.Log("lol2");
                GetElementsFromLayer(_hexLayers[i]);
                return;
            }
        }
        HexLayer newLayer = new HexLayer(xGridLayer, zGridLayer, xOffset, zOffset, _hexLayers.Count);
        _hexLayers.Add(newLayer);
        CreateElements(newLayer);
    }

    private void GetElementsFromLayer(HexLayer hexLayer)
    {
        for (int i = 0; i < hexLayer.xSize; i++)
        {
            for (int j = 0; j < hexLayer.zSize; j++)
            {
                Hex hexToCreate = hexLayer.ActiveHexes[i,j];
                Debug.Log(hexToCreate);
                Debug.Log(hexLayer.HexPropertiesIndex);
                hexToCreate.ChangeProperties(hexLayer.HexPropertiesIndex);
            }
        }
    }

    private void CreateElements(HexLayer currentLayer, bool initial = false)
    {
        for (int i = 0; i < xGridLayer; i++)
        {
            for (int j = 0; j < zGridLayer; j++)
            {
                int xOffset = currentLayer.xIndex * currentLayer.xSize;
                int zOffset = currentLayer.zIndex * currentLayer.zSize;
                Coordinates randomCoordinate = GetRandomCoordinate();
                Vector3 hexPosition = GetPosition(randomCoordinate.X + xOffset, randomCoordinate.Z + zOffset);
                if (initial)
                {
                    Hex hexToCreate = GetElementToCreate();
                    _activeLayer[i, j] = Instantiate(hexToCreate, hexPosition, Quaternion.identity);
                    _activeLayer[i, j].SetupProperties(i, j, hexPosition);
                    _activeLayer[i, j].SetupColor();
                }
                else
                {
                    _activeLayer[i, j].SetupProperties(i + currentLayer.xIndex, j + currentLayer.zIndex, hexPosition);
                    _activeLayer[i,j].ChangeProperties(currentLayer.HexPropertiesIndex);
                }
                currentLayer.ActiveHexes = _activeLayer;
            }
        }
    }

    private Hex GetElementToCreate()
    {
        Hex hex = _hexesToCreate[Random.Range(0, _hexesToCreate.Count)];
        return hex;
    }

    private void CreateListOfElements()
    {
        for (int i = 0; i < hexes.Count; i++)
        {
            for (int j = 0; j < hexes[i].spawnRatio; j++)
            {
                _hexesToCreate.Add(hexes[i].hex);
            }
        }
    }

    private Coordinates GetRandomCoordinate()
    {
        if (_availableCoordinates.Count == 0)
        {
            _availableCoordinates.AddRange(_startingCoordinates);
        }
        Coordinates coordinate = _availableCoordinates[Random.Range(0, _availableCoordinates.Count)];
        _availableCoordinates.Remove(coordinate);
        return coordinate;
    }
    public Vector3 GetPosition(int x, int z)
    {
        return new Vector3(x * xCellSize, 0, z * zCellSize) + ((z % 2) == 1 ? new Vector3(xCellSize, 0, 0) * HEX_OFFSET : Vector3.zero);
    }
}
