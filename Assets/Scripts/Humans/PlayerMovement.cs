using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GridObject _gridObject;

    List<Vector3> _pathWorldPositions;

    [SerializeField]
    float _moveSpeed = 1f;

    public bool IsMoving;

    private void Awake()
    {
        _gridObject = GetComponent<GridObject>();
    }

    public void Move(List<PathNode> path)
    {
        _pathWorldPositions = _gridObject.TargetGrid.ConvertPathNodesToWorldPositions(path);

        _gridObject.PositionOnGrid.x = path[path.Count - 1].Pos_X;
        _gridObject.PositionOnGrid.y = path[path.Count - 1].Pos_Y;
    }

    private void Update()
    {
        if (_pathWorldPositions == null || _pathWorldPositions.Count == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
        {
            _pathWorldPositions.RemoveAt(0);
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }
    }
}
