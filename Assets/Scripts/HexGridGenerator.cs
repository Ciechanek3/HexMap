using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    public List<HexWithSpawnRatio> hexes;
    [Header("Sizes")]
    public float xCellSize;
    public float yCellSize;
    public int xGridSize;
    public int zGridSize; 
    public const float HEX_OFFSET = .5f;

    private List<Hex> _hexesToCreate = new List<Hex>();

    private void Awake()
    {
        CreateListOfElements();
        Debug.Log(_hexesToCreate.Count);
        for (int i = 0; i < xGridSize; i++)
        {
            for (int j = 0; j < zGridSize; j++)
            {
                Hex hexToCreate = GetElementToCreate();
                Instantiate(hexToCreate, GetPosition(j, i), Quaternion.identity);
            }
        }
    }

    private Hex GetElementToCreate()
    {
        if (_hexesToCreate.Count == 0)
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

    public Vector3 GetPosition(int x, int z)
    {
        return new Vector3(x * xCellSize, 0, z * yCellSize) + ((z % 2) == 1 ? new Vector3(xCellSize, 0, 0) * HEX_OFFSET : Vector3.zero);
    }
}
