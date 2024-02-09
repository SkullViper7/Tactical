using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] _grid;

    public int Width = 10;
    public int Length = 10;
    [SerializeField]
    float _cellSize = 1;

    [SerializeField]
    LayerMask _obstacleLayer;
    [SerializeField]
    LayerMask _terrainLayer;

    private void Awake()
    {
        GenerateGrid();
    }

    /// <summary>
    /// Generates the grid with the specified width and length.
    /// </summary>
    void GenerateGrid()
    {
        // Create a new grid of nodes with the specified Length and Width
        _grid = new Node[Length, Width];

        // Initialize each node in the grid with a new Node instance
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                _grid[j, i] = new Node();
            }
        }

        // Calculate the elevation for each node in the grid
        CalculateElevation();

        // Check the terrain passability for each node in the grid
        CheckPassableTerrain();
    }

    /// <summary>
    /// Calculates the elevation for each node in the grid.
    /// </summary>
    void CalculateElevation()
    {
        // Iterate through each position in the grid to cast a ray from above to determine the elevation
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                // Create a ray from the world position at height 100 above the grid position, pointing downwards
                Ray ray = new Ray(GetWorldPosition(j, i) + Vector3.up * 100, Vector3.down);
                RaycastHit hit;

                // Perform a raycast to detect the terrain and update the elevation of the corresponding node in the grid
                if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
                {
                    _grid[j, i].Elevation = hit.point.y;
                }
            }
        }
    }

    /// <summary>
    /// Checks the passable terrain for each node in the grid.
    /// </summary>
    void CheckPassableTerrain()
    {
        // Iterate through each position in the grid to check for passable terrain and update the corresponding node in the grid
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                // Get the world position at the grid coordinates
                Vector3 worldPosition = GetWorldPosition(j, i);

                // Check if there are any obstacles at the world position using a small box (half the cell size) and update the passability of the node
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * _cellSize, Quaternion.identity, _obstacleLayer);
                _grid[j, i].Passable = passable;
            }
        }
    }

    /// <summary>
    /// Draws the grid in the editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        // If the grid is null, draw small cubes for each position in the grid for visualization
        if (_grid == null)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Vector3 pos = GetWorldPosition(j, i);
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
        // If the grid is not null, draw small cubes for each position in the grid with color indicating passability for visualization
        else
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Vector3 pos = GetWorldPosition(j, i, true);
                    Gizmos.color = _grid[j, i].Passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
    }

    /// <summary>
    /// Checks if the position is within the grid bounds.
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posY"></param>
    /// <returns></returns>
    internal bool CheckBoundary(int posX, int posY)
    {
        // Check if the given posX is out of bounds (less than 0 or greater than or equal to Length), and return false if it is
        if (posX < 0 || posX >= Length)
        {
            return false;
        }

        // Check if the given posY is out of bounds (less than 0 or greater than or equal to Width), and return false if it is
        if (posY < 0 || posY >= Width)
        {
            return false;
        }

        // Return true if the given posX and posY are within the bounds of the grid
        return true;
    }

    /// <summary>
    /// Checks if the position is within the grid bounds.
    /// </summary>
    /// <param name="positionOnGrid"></param>
    /// <returns></returns>
    public bool CheckBoundary(Vector2Int positionOnGrid)
    {
        // Check if the x-coordinate of the positionOnGrid is out of bounds (less than 0 or greater than or equal to Length), and return false if it is
        if (positionOnGrid.x < 0 || positionOnGrid.x >= Length)
        {
            return false;
        }

        // Check if the y-coordinate of the positionOnGrid is out of bounds (less than 0 or greater than or equal to Width), and return false if it is
        if (positionOnGrid.y < 0 || positionOnGrid.y >= Width)
        {
            return false;
        }

        // Return true if the x and y coordinates of the positionOnGrid are within the bounds of the grid
        return true;
    }

    /// <summary>
    /// Gets the world position from the grid position.
    /// </summary>
    /// <param name="j"></param>
    /// <param name="i"></param>
    /// <param name="elevation"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(int j, int i, bool elevation = false)
    {
        // Return a new Vector3 with components calculated based on grid positions and elevation condition
        return new Vector3(j * _cellSize, elevation == true ? _grid[j, i].Elevation : 0f, i * _cellSize);
    }

    /// <summary>
    /// Gets the grid position from the world position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        // Adjust the worldPosition to the center of the grid cell
        worldPosition.x += _cellSize / 2;
        worldPosition.z += _cellSize / 2;

        // Calculate the grid position (positionOnGrid) based on the adjusted worldPosition
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / _cellSize), (int)(worldPosition.z / _cellSize));

        // Return the calculated positionOnGrid as a Vector2Int
        return positionOnGrid;
    }

    /// <summary>
    /// Places an object on the grid.
    /// </summary>
    /// <param name="positionOnGrid"></param>
    /// <param name="gridObject"></param>
    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        // Check if the positionOnGrid is within the map boundaries using the CheckBoundary function
        if (CheckBoundary(positionOnGrid))
        {
            // If the position is within the boundaries, assign the gridObject to the GridObject property of the corresponding node in the grid
            _grid[positionOnGrid.x, positionOnGrid.y].GridObject = gridObject;
        }
        else
        {
            // If the position is outside the boundaries, log a message indicating that the object is outside of the map boundaries
            Debug.Log("Object outside of the map boundaries");
        }
    }

    /// <summary>
    /// Gets the object placed on the grid at the given position.
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        // Check if the gridPosition is within the map boundaries using the CheckBoundary function
        if (CheckBoundary(gridPosition))
        {
            // If the position is within the boundaries, retrieve the gridObject from the corresponding node in the grid and return it
            GridObject gridObject = _grid[gridPosition.x, gridPosition.y].GridObject;
            return gridObject;
        }

        // If the position is outside the boundaries, return null
        return null;
    }

    /// <summary>
    /// Checks if the position is walkable.
    /// </summary>
    /// <param name="pos_X"></param>
    /// <param name="pos_Y"></param>
    /// <returns></returns>
    public bool CheckWalkable(int pos_X, int pos_Y)
    {
        // Return the passability value of the node at the specified pos_X and pos_Y in the grid
        return _grid[pos_X, pos_Y].Passable;
    }

    /// <summary>
    /// Converts a list of path nodes to a list of world positions.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
    {
        // Create a list to store world positions
        List<Vector3> worldPositions = new List<Vector3>();

        // Iterate through the path and add the corresponding world position for each node to the list
        for (int i = 0; i < path.Count; i++)
        {
            worldPositions.Add(GetWorldPosition(path[i].Pos_X, path[i].Pos_Y, true));
        }

        // Return the list of world positions
        return worldPositions;
    }
}
