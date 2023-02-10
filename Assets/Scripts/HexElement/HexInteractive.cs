using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInteractive : Hex
{
    public HexDescriptionUI hexDescriptionUI;
    public void OnClick()
    {
        hexDescriptionUI.gameObject.SetActive(true);
        hexDescriptionUI.SetValues(HexProperties.xCoordinate, HexProperties.yCoordinate, color);
    }
}
