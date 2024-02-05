using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField]
    Grid _targetGrid;

    [SerializeField]
    GridObject _targetCharacter;

    [SerializeField]
    LayerMask _terrainLayer;

    PathFinding _pathFindingScript;

    List<PathNode> _path;

    private void Start()
    {
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
    }

    public void OnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
        {
            Vector2Int gridPosition = _targetGrid.GetGridPosition(hit.point);

            _path = _pathFindingScript.FindPath(_targetCharacter.PositionOnGrid.x, _targetCharacter.PositionOnGrid.y, gridPosition.x, gridPosition.y);

            if (_path == null || _path.Count == 0)
            {
                return;
            }

            _targetCharacter.GetComponent<PlayerMovement>().Move(_path);
        }
    }
}
