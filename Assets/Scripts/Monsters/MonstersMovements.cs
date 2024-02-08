using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersMovements : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private Grid _targetGrid;

    public GameObject TargetPlayer;
    private GridObject _gridObjectPlayer;
    private GridObject _gridObjectMonster;

    private List<Vector3> _pathWorldPositions;

    private List<PathNode> _path;
    private PathFinding _pathFindingScript;


    private void Awake()
    {
        _gridObjectPlayer = TargetPlayer.GetComponent<GridObject>();
        _gridObjectMonster = GetComponent<GridObject>();
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
    }

    public void Move(List<PathNode> path)
    {
        _pathWorldPositions = _gridObjectMonster.TargetGrid.ConvertPathNodesToWorldPositions(path);

        _gridObjectMonster.PositionOnGrid.x = path[path.Count - 1].Pos_X;
        _gridObjectMonster.PositionOnGrid.y = path[path.Count - 1].Pos_Y;
    }

    public void Test()
    {
        _path = _pathFindingScript.FindPath(_gridObjectMonster.PositionOnGrid.x, _gridObjectMonster.PositionOnGrid.y, _gridObjectPlayer.PositionOnGrid.x, _gridObjectPlayer.PositionOnGrid.y);

        if (_path == null || _path.Count == 0)
        {
            return;
        }

        _path.RemoveAt(_path.Count-1);

        Move(_path);
    }

    private void Update()
    {
        if (_pathWorldPositions == null || _pathWorldPositions.Count == 0)
        {
            return;
        }

        if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
        {
            _pathWorldPositions.RemoveAt(0);
        }

        if (_pathWorldPositions.Count == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);
    }
}
