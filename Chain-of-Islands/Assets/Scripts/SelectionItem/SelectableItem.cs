using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    public Item item;

    public void SelectItem()
    {
        gameObject.SetActive(false);
    }
}
