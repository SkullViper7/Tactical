using System;
using System.Collections;
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

    void GenerateGrid()
    {
        _grid = new Node[Length, Width];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                _grid[j, i] = new Node();
            }
        }

        CalculateElevation();
        CheckPassableTerrain();
    }

    void CalculateElevation()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                Ray ray = new Ray(GetWorldPosition(j, i) + Vector3.up * 100, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
                {
                    _grid[j, i].Elevation = hit.point.y;
                }
            }
        }
    }

    void CheckPassableTerrain()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Length; j++)
            {
                Vector3 worldPosition = GetWorldPosition(j, i);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * _cellSize, Quaternion.identity, _obstacleLayer);
                _grid[j, i].Passable = passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
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

    internal bool CheckBoundary(int posX, int posY)
    {
        if (posX < 0 || posX >= Length)
        {
            return false;
        }

        if (posY < 0 || posY >= Width)
        {
            return false;
        }

        return true;
    }

    public bool CheckBoundary(Vector2Int positionOnGrid)
    {
        if (positionOnGrid.x < 0 || positionOnGrid.x >= Length)
        {
            return false;
        }

        if (positionOnGrid.y < 0 || positionOnGrid.y >= Width)
        {
            return false;
        }

        return true;
    }

    public Vector3 GetWorldPosition(int j, int i, bool elevation = false)
    {
        return new Vector3(j * _cellSize, elevation == true ? _grid[j, i].Elevation : 0f, i * _cellSize);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition.x += _cellSize / 2;
        worldPosition.z += _cellSize / 2;
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

    public bool CheckWalkable(int pos_X, int pos_Y)
    {
        return _grid[pos_X, pos_Y].Passable;
    }

    public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
    {
        List<Vector3> worldPositions = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPositions.Add(GetWorldPosition(path[i].Pos_X, path[i].Pos_Y, true));
        }

        return worldPositions;
    }
}
