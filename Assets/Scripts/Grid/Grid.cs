using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] _grid;

    [SerializeField]
    int _width = 10;
    [SerializeField]
    int _length = 10;
    [SerializeField]
    float _cellSize = 1;

    [SerializeField]
    LayerMask _obstacleLayer;

    private void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _grid = new Node[_length, _width];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                _grid[j, i] = new Node();
            }
        }

        CheckPassableTerrain();
    }

    void CheckPassableTerrain()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                Vector3 worldPosition = GetWorldPosition(j, i);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * _cellSize, Quaternion.identity, _obstacleLayer);
                _grid[j, i] = new Node();
                _grid[j, i].Passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_grid == null)
        {
            return;
        }

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                Vector3 pos = GetWorldPosition(j, i);
                Gizmos.color = _grid[j, i].Passable ? Color.white : Color.red;
                Gizmos.DrawCube(pos, Vector3.one / 4);
            }
        }
    }

    bool CheckBoundary(Vector2Int positionOnGrid)
    {
        if (positionOnGrid.x < 0 || positionOnGrid.x >= _length)
        {
            return false;
        }

        if (positionOnGrid.y < 0 || positionOnGrid.y >= _width)
        {
            return false;
        }

        return true;
    }

    Vector3 GetWorldPosition(int j, int i)
    {
        return new Vector3(transform.position.x + (j * _cellSize), 0, transform.position.z + (i * _cellSize));
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition -= transform.position;
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / _cellSize), (int)(worldPosition.z / _cellSize));
        return positionOnGrid;
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundary(positionOnGrid))
        {
            _grid[positionOnGrid.x, positionOnGrid.y].GridObject = gridObject;
        }

        else
        {
            Debug.Log("Object outside of the map boundaries");
        }
    }

    public GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        if (CheckBoundary(gridPosition))
        {
            GridObject gridObject = _grid[gridPosition.x, gridPosition.y].GridObject;
            return gridObject;
        }
        return null;
    }
}
