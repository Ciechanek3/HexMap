using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    [SerializeField]
    private List<HexWithSpawnRatio> hexes;
    [SerializeField]
    private HexGridLayerManagement hexGridLayerManagement;
    [SerializeField]
    private HexDescriptionUI hexDescriptionUI;
    [Header("Sizes")]

    public float xCellSize;
    public float zCellSize;

    [SerializeField]
    private int xGridSize;
    [SerializeField]
    private int zGridSize;

    public const float HEX_OFFSET = .5f;

    private List<Hex> _hexesToCreate = new List<Hex>();
    private Queue<Hex[,]> _activeLayers = new Queue<Hex[,]>();

    public float GridLength => xGridSize * xCellSize;
    public float GridHeight => zGridSize * zCellSize;

    private List<Coordinates> _availableCoordinates = new List<Coordinates>();
    private List<Coordinates> _startingCoordinates = new List<Coordinates>();

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
        for (int i = 0; i < hexGridLayerManagement.xGridLayer; i++)
        {
            for (int j = 0; j < hexGridLayerManagement.zGridLayer; j++)
            {
                Coordinates coordinates = new Coordinates(i, j);
                _availableCoordinates.Add(coordinates);
            }
        }
        _startingCoordinates.AddRange(_availableCoordinates);

        CreateListOfElements();
    }

    public void GetLayer(int xOffset, int zOffset)
    {
        for (int i = 0; i < hexGridLayerManagement.HexLayers.Count; i++)
        {
            if (hexGridLayerManagement.HexLayers[i].xIndex == xOffset && hexGridLayerManagement.HexLayers[i].zIndex == zOffset)
            {
                GetElementsFromLayersList(hexGridLayerManagement.HexLayers[i]);
                return;
            }
        }
        HexLayer newLayer = new HexLayer(hexGridLayerManagement.xGridLayer, hexGridLayerManagement.zGridLayer, xOffset, zOffset, hexGridLayerManagement.HexLayers.Count);
        hexGridLayerManagement.HexLayers.Add(newLayer);
        CreateElements(newLayer);
    }

    private void GetElementsFromLayersList(HexLayer hexLayer)
    {
        for (int i = 0; i < hexLayer.xSize; i++)
        {
            for (int j = 0; j < hexLayer.zSize; j++)
            {
                hexLayer.ChangePropertiesOfHex(i, j);
            }
        }
    }
    
    public void CreateElements(HexLayer currentLayer, bool initial = false)
    {
        Hex[,] activeLayer;
        if (initial)
        {
            Hex[,] newLayer = new Hex[hexGridLayerManagement.xGridLayer, hexGridLayerManagement.zGridLayer];
            activeLayer = newLayer;
        }
        else
        {
            activeLayer = _activeLayers.Dequeue();
        }
        
        for (int i = 0; i < hexGridLayerManagement.xGridLayer; i++)
        {
            for (int j = 0; j < hexGridLayerManagement.zGridLayer; j++)
            {
                int xOffset = currentLayer.xIndex * currentLayer.xSize;
                int zOffset = currentLayer.zIndex * currentLayer.zSize;
                Coordinates randomCoordinate = GetRandomCoordinate();
                Vector3 hexPosition = GetPosition(randomCoordinate.X + xOffset, randomCoordinate.Z + zOffset);
                if (initial)
                {
                    Hex hexToCreate = GetElementToCreate();
                    TryAddUIToInteractive(hexToCreate);
                    activeLayer[i, j] = Instantiate(hexToCreate, hexPosition, Quaternion.identity);
                    currentLayer.SetupProperties(i, j, i + xOffset, j + zOffset, hexPosition);
                    activeLayer[i, j].SetupColor();
                }
                else
                {
                    currentLayer.SetupProperties(i, j, i + currentLayer.xIndex, j + currentLayer.zIndex, hexPosition);
                    activeLayer[i,j].ChangeProperties(hexPosition);
                } 
            }
        }
        currentLayer.ActiveHexes = activeLayer;
        currentLayer.SetProperties();


        _activeLayers.Enqueue(currentLayer.ActiveHexes);
    }

    private Hex TryAddUIToInteractive(Hex hex)
    {
        HexInteractive hexInteractive = hex as HexInteractive;
        if (hexInteractive != null)
        {
            hexInteractive.hexDescriptionUI = hexDescriptionUI;
        }
        else
        {
            return hex;
        }

        return hexInteractive;
    }
    private Hex GetElementToCreate()
    {
        if(_hexesToCreate.Count == 0)
        {
            CreateListOfElements();
        }
        Hex hex = _hexesToCreate[Random.Range(0, _hexesToCreate.Count)];
        _hexesToCreate.Remove(hex);
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
