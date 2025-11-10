using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private float _speedMob;
    [SerializeField] private MobAI _mobAI;

    private void Update()
    {
        _mobAI.ChooseState();
    }
}
