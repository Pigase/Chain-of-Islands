using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMovmentInArea : MonoBehaviour
{
    [SerializeField] private Collider2D _movementArea;
    [SerializeField] private float _timeToWait;
    //чтоб можно было менять частоту остановок

    private Vector2 _targetPosition;
    private bool _hasTarget = false;
    private bool _wait = false;

    private void Awake()
    {
        SetNewTargetInArea();
    }

    public Vector2 ChooseTargetInArea(GameObject targetObject)
    {

        if (!_hasTarget || Vector2.Distance(targetObject.transform.position, _targetPosition) < 0.1f)
        {
            float temp = Random.Range(1, 10);
            if (temp >= 5f)
            {
                _wait = true;
            }
            if (_wait)
            {
                StartCoroutine("Wait");
            }
            else
            {
                SetNewTargetInArea();

            }
        }

        return _targetPosition;
    }

    private void SetNewTargetInArea()
    {
        if (_movementArea != null)
        {
            while (true)
            {
                Bounds bounds = _movementArea.bounds;
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float y = Random.Range(bounds.min.y, bounds.max.y);
                _targetPosition = new Vector2(x, y);
                _hasTarget = true;
                if (_movementArea.OverlapPoint(_targetPosition))
                {
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Movement Area is not assigned!"); 
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_timeToWait);
        _wait = false;
    }

    // Визуализация цели в редакторе я пидорас
    private void OnDrawGizmos()
    {
        if (_hasTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetPosition, 0.2f);
        }
    }
}