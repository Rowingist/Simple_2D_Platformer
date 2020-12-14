using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Vector3[] _waypoints;

    private float _timeOfMovement = 5f;

    private void Start()
    {
        Tween tween = transform.DOPath(_waypoints, _timeOfMovement, PathType.Linear, PathMode.Sidescroller2D).SetOptions(true, AxisConstraint.Y).SetLookAt(0.01f); 

        tween.SetEase(Ease.Linear).SetLoops(-1);
    }
}
