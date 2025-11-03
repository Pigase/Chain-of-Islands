using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private float _speedMob;
    [SerializeField] private BrainMovmentInArea _brainMovmentInArea;

    private Vector2 _moveDirectionPlayer;
    private Mover _mover;

    private void Awake()
    {
        _mover = new Mover(gameObject);
    }
    private void Update()
    {
        _moveDirectionPlayer=_brainMovmentInArea.ChooseTargetInArea(gameObject);

        _mover.MoveObjectToPoint(_speedMob, _moveDirectionPlayer);
    }
}
