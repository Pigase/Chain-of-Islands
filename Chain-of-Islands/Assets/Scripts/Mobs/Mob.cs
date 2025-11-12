using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private MobAI _mobAI;

    private void Update()
    {
        if (_mobAI != null)
        {
            _mobAI.ChooseState();
        }
    }
}