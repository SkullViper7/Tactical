using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition;

    Grid _targetGrid;

    private void Start()
    {
        _targetGrid = GameObject.Find("Grid").GetComponent<Grid>();
        GridPosition = _targetGrid.GetGridPosition(transform.position);
    }
}
