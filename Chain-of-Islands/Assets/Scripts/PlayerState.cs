using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    void Enter();
    void Update();
    void Exit();
}