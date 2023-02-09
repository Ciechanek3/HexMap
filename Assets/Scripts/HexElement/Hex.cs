using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;

    public HexProperties _hexProperties;

    public void SetupProperties(int x, int y)
    {
        _hexProperties = new HexProperties(x, y, _renderer.materials[0].color);
    }
}
