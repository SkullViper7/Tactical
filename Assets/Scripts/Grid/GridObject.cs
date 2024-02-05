using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Grid TargetGrid;

    public Vector2Int PositionOnGrid;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        PositionOnGrid = TargetGrid.GetGridPosition(transform.position);
        TargetGrid.PlaceObject(PositionOnGrid, this);
        Vector3 pos = TargetGrid.GetWorldPosition(PositionOnGrid.x, PositionOnGrid.y, true);
        transform.position = pos;
    }
}
