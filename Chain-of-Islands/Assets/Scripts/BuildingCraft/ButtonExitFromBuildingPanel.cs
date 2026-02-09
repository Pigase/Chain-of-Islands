using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitFromBuildingPanel : MonoBehaviour
{
    public event Action OnExitedFromCraftPanel;

    public void ExitFromPanel()
    {
        OnExitedFromCraftPanel?.Invoke();
    }
}
