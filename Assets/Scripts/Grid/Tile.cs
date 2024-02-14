using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition;

    Grid _targetGrid;

    public bool IsReachable;

    public MeshRenderer TileMeshRenderer;

    /// <summary>
    /// Gets the required elements like the grid and the mesh renderer. Gets the grid position of the tile.
    /// </summary>
    private void Start()
    {
        _targetGrid = GameObject.Find("Grid").GetComponent<Grid>();
        GridPosition = _targetGrid.GetGridPosition(transform.position);
    }
}
