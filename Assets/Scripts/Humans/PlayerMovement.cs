using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GridObject _gridObject;

    List<Vector3> _pathWorldPositions;

    [SerializeField]
    float _moveSpeed = 1f;

    [HideInInspector]
    public bool IsMoving;

    private void Awake()
    {
        _gridObject = GetComponent<GridObject>();
    }

    /// <summary>
    /// Converts a list of path nodes to a list of world positions.
    /// </summary>
    /// <param name="path"></param>
    public void Move(List<PathNode> path)
    {
        // Convert the path nodes to world positions using the TargetGrid's method and assign the result to _pathWorldPositions
        _pathWorldPositions = _gridObject.TargetGrid.ConvertPathNodesToWorldPositions(path);

        // Set the x coordinate of the PositionOnGrid property of _gridObject to the x coordinate of the last node in the path
        _gridObject.PositionOnGrid.x = path[path.Count - 1].Pos_X;

        // Set the y coordinate of the PositionOnGrid property of _gridObject to the y coordinate of the last node in the path
        _gridObject.PositionOnGrid.y = path[path.Count - 1].Pos_Y;
    }

    /// <summary>
    /// Moves the player along the path and stops when it reaches the end of the path.
    /// </summary>
    private void Update()
    {
        // If the _pathWorldPositions list is null or empty, exit the function
        if (_pathWorldPositions == null || _pathWorldPositions.Count == 0)
        {
            return;
        }

        // Move the current object's position towards the first point in the _pathWorldPositions list at a speed determined by _moveSpeed and Time.deltaTime
        transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);

        // If the distance between the current position and the first point in _pathWorldPositions is less than 0.05 units
        if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
        {
            // Remove the first point from _pathWorldPositions and set IsMoving to false
            _pathWorldPositions.RemoveAt(0);
            IsMoving = false;
        }
        else
        {
            // Set IsMoving to true if the object is still moving towards the next point
            IsMoving = true;
        }
    }
}
