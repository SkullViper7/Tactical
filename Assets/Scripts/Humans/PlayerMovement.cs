using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GridObject _gridObject;

    [SerializeField]
    Grid _grid;

    List<Vector3> _pathWorldPositions;

    [SerializeField]
    float _moveSpeed = 1f;

    [HideInInspector]
    public bool IsMoving;

    Animator _animator;

    // Adjust 'turnSpeed' to change how quickly the player turns: larger values will make the turn faster.
    [SerializeField] private float turnSpeed = 10f;

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

        // Storing the animator component of the player
        _animator = PlayerManager.Instance.HmnGrid.GetComponentInChildren<Animator>();

        StartCoroutine(MoveObject());
    }

    /// <summary>
    /// Moves the player along the path and stops when it reaches the end of the path.
    /// </summary>
    private IEnumerator MoveObject()
    {
        while (_pathWorldPositions.Count > 0)
        {
            IsMoving = true;
            PlayerManager.Instance.CanFindPath = false;

            // Calculation of the direction to the next target position.
            Vector3 targetDirection = _pathWorldPositions[0] - transform.position;

            // Check if the direction is not zero (the player is not already at the destination).
            if (targetDirection != Vector3.zero)

            {

                // Calculate the rotation needed to look in the direction of the target.
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                targetRotation *= Quaternion.Euler(0, -90, 0);



                // Interpolate to this rotation to smooth the transition using Quaternion.Lerp or Quaternion.Slerp.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            // Move the current object's position towards the first point in the _pathWorldPositions list at a speed determined by _moveSpeed and Time.deltaTime
            transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);


            // Check if the object is still moving towards the next point
            if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
            {
                // Remove the first point from _pathWorldPositions and set IsMoving to false
                _pathWorldPositions.RemoveAt(0);

                // Update animation state to Idle
                StartCoroutine(AnimationManager.Instance.UpdateAnimState(_animator, 0, 0));

                _grid.CheckPassableTerrain();
            }
            else
            {
                // Update animation state to Run
                StartCoroutine(AnimationManager.Instance.UpdateAnimState(_animator, 1, 0));
            }

            yield return null;
        }

        PlayerManager.Instance.CanFindPath = true;
        IsMoving = false;
    }
}
