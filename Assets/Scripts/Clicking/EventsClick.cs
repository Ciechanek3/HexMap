using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private HexInteractive hexInteractive;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            hexInteractive.OnClick();
        }
    }
}
