using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighlightPath : MonoBehaviour
{
    [Header("Terrain/Grid")]
    [SerializeField]
    LayerMask _terrainLayer;

    [SerializeField]
    Grid _targetGrid;

    public List<PathNode> Path;

    PathFinding _pathFindingScript;

    [Header("Scripts Reference")]
    [SerializeField]
    PlayerMovement _playerMovementScript;

    List<Tile> _tiles = new List<Tile>();

    [Header("Rendering")]
    [SerializeField]
    Material _baseMat;
    [SerializeField]
    Material _goodHighlightMat;
    [SerializeField]
    Material _badHighlightMat;

    int _currentMovementPoints = 5;
    int _currentActionPoints = 5;

    public bool CanClick = true;

    PlayerInput _playerInput;

    /// <summary>
    /// Adding tiles to the list. Registering the OnPoint function to the input system.
    /// </summary>
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

    /// <summary>
    /// Called when the mouse is moved. Casts a ray from the camera to the mouse position. Calculates the path to the mouse position. Checks if the path is reachable.
    /// Modifs the material of the tiles based on the path and their position in the list.
    /// </summary>
    /// <param name="context"></param>
    public void OnPoint(InputAction.CallbackContext context)
    {
        if (PlayerManager.Instance.CanFindPath)
        {
            // Create a ray from the main camera to the current mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Cast a ray from the camera to find the objects hit by the ray within a maximum distance and with a the Terrain layer mask
            if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
            {
                // Get the grid position at the hit point using the target grid
                Vector2Int gridPosition = _targetGrid.GetGridPosition(hit.point);

                // Find a path from the current character's position to the grid position using the path finding script
                Path = _pathFindingScript.FindPath(PlayerManager.Instance.HmnGrid.PositionOnGrid.x, PlayerManager.Instance.HmnGrid.PositionOnGrid.y, gridPosition.x, gridPosition.y);

                // Check if the hit object's Tile component is reachable
                if (hit.transform.gameObject.GetComponent<Tile>().IsReachable)
                {
                    CanClick = true;
                }
                else
                {
                    CanClick = false;
                }
            }

            // If no path is found or the path is empty, exit the function
            if (Path == null || Path.Count == 0)
            {
                return;
            }

            // Create a HashSet of grid positions from the found path
            HashSet<Vector2Int> pathPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < Path.Count; i++)
            {
                pathPositions.Add(new Vector2Int(Path[i].Pos_X, Path[i].Pos_Y));
            }

            // Iterate through the tiles and update their visual and reachable properties based on the path
            for (int i = 0; i < _tiles.Count; i++)
            {
                if (pathPositions.Contains(_tiles[i].GridPosition))
                {
                    if (!_playerMovementScript.IsMoving || _tiles[i].GridPosition == new Vector2Int(Path[0].Pos_X, Path[0].Pos_Y))
                    {
                        // Find the index of the current tile in the path
                        int pathNodeIndex = Path.FindIndex(p => p.Pos_X == _tiles[i].GridPosition.x && p.Pos_Y == _tiles[i].GridPosition.y);
                        // Check if the index exceeds the current movement points, update the tile's material and reachability
                        if (pathNodeIndex >= _currentMovementPoints && PlayerManager.Instance.IsMoving || pathNodeIndex >= _currentActionPoints && !PlayerManager.Instance.IsMoving)
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
                        // Reset the tile's material to the base material
                        _tiles[i].TileMeshRenderer.material = _baseMat;
                    }
                }
                else
                {
                    // Reset the tile's material to the base material
                    _tiles[i].TileMeshRenderer.material = _baseMat;
                }
            }
        }
    }

    /// <summary>
    /// Disables highlight when the player is moving.
    /// </summary>
    public void DisableHighights()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            // Reset the tile's material to the base material
            _tiles[i].TileMeshRenderer.material = _baseMat;
        }
    }
}
