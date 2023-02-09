using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;

    public HexProperties HexProperties;
    public Color color;

    private List<HexProperties> _hexPropertiesList = new List<HexProperties>();
    public void SetupProperties(int x, int y, Vector3 p)
    {
        HexProperties = new HexProperties(x, y, p);
        _hexPropertiesList.Add(HexProperties);
    }

    public void SetupColor()
    {
        color = _renderer.materials[0].color;
    }

    public void ChangeProperties(int index)
    {
        Debug.Log(index);
        HexProperties = _hexPropertiesList[index];
        transform.position = HexProperties.Position;
    }
}
