using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examination : MonoBehaviour
{
    [SerializeField] private string _itemId;
    public void Exam()
    {
        ItemDataBase.Instance.TestFindItem(_itemId);
    }
}
