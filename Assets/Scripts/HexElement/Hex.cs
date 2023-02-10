using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;

    public HexProperties HexProperties;

    public Color color;

    public void SetupColor()
    {
        color = _renderer.materials[0].color;
    }


    public void ChangeProperties(Vector3 position)
    {
        this.transform.position = position;
    }

    public void SetupProperty(HexProperties hexProperties)
    {
        HexProperties = hexProperties;
    }
}
