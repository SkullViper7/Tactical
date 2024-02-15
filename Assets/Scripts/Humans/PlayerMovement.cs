using System.Collections;
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

        StartCoroutine(MoveObject());
    }

    /// <summary>
    /// Moves the player along the path and stops when it reaches the end of the path.
    /// </summary>
    private IEnumerator MoveObject()
    {
        while (_pathWorldPositions.Count > 0)
        {
            // Move the current object's position towards the first point in the _pathWorldPositions list at a speed determined by _moveSpeed and Time.deltaTime
            transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);

            // Check if the object is still moving towards the next point
            if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
            {
                // Remove the first point from _pathWorldPositions and set IsMoving to false
                _pathWorldPositions.RemoveAt(0);
                IsMoving = false;

                // Update animation state to Idle
                AnimationManager.Instance.UpdateAnimState(PlayerManager.Instance.HmnGrid.GetComponentInChildren<Animator>(), 0);
            }
            else
            {
                // Set IsMoving to true if the object is still moving towards the next point
                IsMoving = true;

                // Update animation state to Run
                AnimationManager.Instance.UpdateAnimState(PlayerManager.Instance.HmnGrid.GetComponentInChildren<Animator>(), 4);
            }

            yield return null;
        }
    }
}
