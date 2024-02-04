using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField]
    Grid _targetGrid;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        Vector2Int positionOnGrid = _targetGrid.GetGridPosition(transform.position);
        _targetGrid.PlaceObject(positionOnGrid, this);
    }
}
