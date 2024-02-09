using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighlightPath : MonoBehaviour
{
    [SerializeField]
    LayerMask _terrainLayer;

    [SerializeField]
    Grid _targetGrid;

    public List<PathNode> Path;

    PathFinding _pathFindingScript;

    [SerializeField]
    PlayerMovement _playerMovementScript;

    [SerializeField]
    GridObject _targetCharacter;

    [SerializeField]
    List<GameObject> _tiles;

    [SerializeField]
    Material _baseMat;
    [SerializeField]
    Material _goodHighlightMat;
    [SerializeField]
    Material _badHighlightMat;

    int _currentMovementPoints = 5;

    public bool CanMove = true;

    private void Start()
    {
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
        {
            Vector2Int gridPosition = _targetGrid.GetGridPosition(hit.point);

            Path = _pathFindingScript.FindPath(_targetCharacter.PositionOnGrid.x, _targetCharacter.PositionOnGrid.y, gridPosition.x, gridPosition.y);

            if (hit.transform.gameObject.GetComponent<Tile>().IsReachable)
            {
                CanMove = true;
            }
            else
            {
                CanMove = false;
            }
        }

        if (Path == null || Path.Count == 0)
        {
            return;
        }

        HashSet<Vector2Int> pathPositions = new HashSet<Vector2Int>();
        foreach (PathNode pathNode in Path)
        {
            pathPositions.Add(new Vector2Int(pathNode.Pos_X, pathNode.Pos_Y));
        }

        foreach (GameObject tile in _tiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (pathPositions.Contains(tileComponent.GridPosition))
            {
                if (!_playerMovementScript.IsMoving || tileComponent.GridPosition == new Vector2Int(Path[0].Pos_X, Path[0].Pos_Y))
                {
                    int pathNodeIndex = Path.FindIndex(p => p.Pos_X == tileComponent.GridPosition.x && p.Pos_Y == tileComponent.GridPosition.y);
                    if (pathNodeIndex >= _currentMovementPoints)
                    {
                        tile.GetComponent<MeshRenderer>().material = _badHighlightMat;
                        tile.GetComponent<Tile>().IsReachable = false;
                    }
                    else
                    {
                        tile.GetComponent<MeshRenderer>().material = _goodHighlightMat;
                        tile.GetComponent<Tile>().IsReachable = true;
                    }
                }
                else
                {
                    tile.GetComponent<MeshRenderer>().material = _baseMat;
                }
            }
            else
            {
                tile.GetComponent<MeshRenderer>().material = _baseMat;
            }
        }
    }

    public void DisableHighights()
    {
        foreach (GameObject tile in _tiles)
        {
            tile.GetComponent<MeshRenderer>().material = _baseMat;
        }
    }
}
