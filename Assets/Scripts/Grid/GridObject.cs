using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Grid TargetGrid;

    public Vector2Int PositionOnGrid;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Initializes the object by placing it on the grid based on its transform position.
    /// </summary>
    void Init()
    {
        // Get the grid position of the object and place it on the target grid
        PositionOnGrid = TargetGrid.GetGridPosition(transform.position);
        TargetGrid.PlaceObject(PositionOnGrid, this);

        // Get the world position from the grid position and update the object's transform position
        Vector3 pos = TargetGrid.GetWorldPosition(PositionOnGrid.x, PositionOnGrid.y, true);
        transform.position = pos;
    }
}
