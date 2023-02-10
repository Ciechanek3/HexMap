using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HexDescriptionUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI xCoordinateText;
    [SerializeField]
    private TextMeshProUGUI zCoordinateText;
    [SerializeField]
    private Image colorImage;

    public void SetValues(int x, int z, Color color)
    {
        xCoordinateText.SetText("X: " + x.ToString());
        zCoordinateText.SetText("Z: " + z.ToString());
        colorImage.color = color;
    }
}
