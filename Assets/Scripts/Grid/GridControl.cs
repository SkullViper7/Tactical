using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField]
    Grid _targetGrid;

    [SerializeField]
    LayerMask _terrainLayer;

    public void OnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
        {
            Vector2Int gridPosition = _targetGrid.GetGridPosition(hit.point);
            GridObject gridObject = _targetGrid.GetPlacedObject(gridPosition);

            if (gridObject == null )
            {
                Debug.Log("x = " + gridPosition.x + "y = " + gridPosition.y + " is empty");
            }
            else
            {
                Debug.Log("x = " + gridPosition.x + "y = " + gridPosition.y + gridObject.GetComponent<Character>().Name);
            }
        }
    }
}
