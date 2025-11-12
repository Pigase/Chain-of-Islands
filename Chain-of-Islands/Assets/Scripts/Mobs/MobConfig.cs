using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Чтобы видеть в инспекторе
public class MobConfig
{
    [Header("Movement Settings")]
    public float speedRoaming = 1.5f;
    public float speedChasing = 2.5f;

    [Header("Chasing Settings")]
    public float chaseStartDistance = 2f;
    public float chaseExitDistance = 5f;

    [Header("Roaming Settings")]
    public float minRoamDistance = 2f;
    public float maxRoamDistance = 7f;

    [Header("Idle Settings")]
    public float idleTime = 2f;

    [Header("Agent Settings")]
    public float stoppingDistance = 0.5f;

}