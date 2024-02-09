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

    private Monsters _monsters;

    public bool CanAttack = false;
    public bool CanMove = true;


    private void Awake()
    {
        _gridObjectPlayer = TargetPlayer.GetComponent<GridObject>();
        _gridObjectMonster = GetComponent<GridObject>();
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
        _monsters = GetComponent<Monsters>();
    }

    public void Move(List<PathNode> path)
    {
        _pathWorldPositions = _gridObjectMonster.TargetGrid.ConvertPathNodesToWorldPositions(path);
        Debug.Log("move"+_pathWorldPositions.Count);


        _gridObjectMonster.PositionOnGrid.x = path[path.Count - 1].Pos_X;
        _gridObjectMonster.PositionOnGrid.y = path[path.Count - 1].Pos_Y;
    }

    public void FindPath()
    {
        CanMove = true;

        _path = _pathFindingScript.FindPath(_gridObjectMonster.PositionOnGrid.x, _gridObjectMonster.PositionOnGrid.y, _gridObjectPlayer.PositionOnGrid.x, _gridObjectPlayer.PositionOnGrid.y);

        _path.RemoveAt(_path.Count - 1);

        if (_path == null || _path.Count == 0)
        {
               return;
        }
        Debug.Log(_path.Count);

        Move(_path);
    }

    public void TravellingMonster()
    {
        if (_monsters.MonsterPM > 0 && CanMove)
        {
            if (_pathWorldPositions.Count == 0)
            {
                CanAttack = true;
                CanMove = false;
                return;
            }

            if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
            {
                _pathWorldPositions.RemoveAt(0);
                Debug.Log(_pathWorldPositions.Count);

                _monsters.MonsterPM--;
                CanAttack = false;
                
            }
            Debug.Log(_pathWorldPositions.Count);

            transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);

        }
        else
        {
            _pathWorldPositions.Clear();
            CanMove = false;
            return;
        }
    }

    private void Update()
    {
        if (_pathWorldPositions == null || _pathWorldPositions.Count == 0)
        {
            return;
        }

        if (_pathWorldPositions.Count > 0 && CanMove)
        {
            Debug.Log(_pathWorldPositions.Count);

            TravellingMonster();
            Debug.Log(_pathWorldPositions.Count);

        }
        else
        {

        }

    }

    public void AddPM()
    {
        _monsters.MonsterPM = 3;
    }
}
