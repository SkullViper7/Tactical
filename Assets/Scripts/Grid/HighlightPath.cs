using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    List<Tile> _tiles = new List<Tile>();

    [SerializeField]
    Material _baseMat;
    [SerializeField]
    Material _goodHighlightMat;
    [SerializeField]
    Material _badHighlightMat;

    int _currentMovementPoints = 5;

    public bool CanMove = true;

    PlayerInput _playerInput;

    private void Start()
    {
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();

        foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            _tiles.Add(tile.GetComponent<Tile>());
        }

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += OnPoint;
    }

    public void OnPoint(InputAction.CallbackContext context)
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
        for (int i = 0; i < Path.Count; i++)
        {
            pathPositions.Add(new Vector2Int(Path[i].Pos_X, Path[i].Pos_Y));
        }

        for (int i = 0; i < _tiles.Count; i++)
        {
            if (pathPositions.Contains(_tiles[i].GridPosition))
            {
                if (!_playerMovementScript.IsMoving || _tiles[i].GridPosition == new Vector2Int(Path[0].Pos_X, Path[0].Pos_Y))
                {
                    int pathNodeIndex = Path.FindIndex(p => p.Pos_X == _tiles[i].GridPosition.x && p.Pos_Y == _tiles[i].GridPosition.y);
                    if (pathNodeIndex >= _currentMovementPoints)
                    {
                        _tiles[i].TileMeshRenderer.material = _badHighlightMat;
                        _tiles[i].IsReachable = false;
                    }
                    else
                    {
                        _tiles[i].TileMeshRenderer.material = _goodHighlightMat;
                        _tiles[i].IsReachable = true;
                    }
                }
                else
                {
                    _tiles[i].TileMeshRenderer.material = _baseMat;
                }
            }
            else
            {
                _tiles[i].TileMeshRenderer.material = _baseMat;
            }
        }
    }

    public void DisableHighights()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].TileMeshRenderer.material = _baseMat;
        }
    }
}
