using System.Collections.Generic;
using UnityEngine;

public class MonstersMovements : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private Grid _targetGrid;

    public Human _human;
    private GridObject _gridObjectPlayer;
    private GridObject _gridObjectMonster;

    private List<Vector3> _pathWorldPositions;

    private List<PathNode> _path;
    private PathFinding _pathFindingScript;

    private Monsters _monsters;
    private MonsterAttack _monsterAttack;

    public bool CanAttack = false;
    public bool CanMove = true;


    private void Awake()
    {
        _gridObjectMonster = GetComponent<GridObject>();
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
        _monsters = GetComponent<Monsters>();
        _monsterAttack = GetComponent<MonsterAttack>();
    }

    public void Move(List<PathNode> path)
    {
        CanMove = true;

        _pathWorldPositions = _gridObjectMonster.TargetGrid.ConvertPathNodesToWorldPositions(path);

        _gridObjectMonster.PositionOnGrid.x = path[path.Count - 1].Pos_X;
        _gridObjectMonster.PositionOnGrid.y = path[path.Count - 1].Pos_Y;

        TravellingMonster();
    }

    public void TravellingMonster()
    {
        if (_monsters.MonsterPM > 0 && CanMove)
        {
            if (_pathWorldPositions.Count == 0)
            {
                CanMove = false;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
            {
                _pathWorldPositions.RemoveAt(0);
                _monsters.MonsterPM--;
                CanAttack = false;
            }

            if (Vector3.Distance(transform.position, _human.transform.position) < 2f && _monsters.MonsterPA > 0)
            {
                _monsterAttack.UseAttack(_monsters, _human);
            }

        }
        else
        {
            CanMove = false;
            return;
        }
    }

    public void SearchPlayerNearby()
    {
        GameObject[] joueurs = GameObject.FindGameObjectsWithTag("Player");
        List<PathNode> _finalPath = new List<PathNode>();


        foreach (GameObject joueur in joueurs)
        {
            List<PathNode> _currentPath = new List<PathNode>();

            GridObject _gridObjectPlayerTempo = joueur.GetComponent<GridObject>();
            _currentPath = _pathFindingScript.FindPath(_gridObjectMonster.PositionOnGrid.x, _gridObjectMonster.PositionOnGrid.y, _gridObjectPlayerTempo.PositionOnGrid.x, _gridObjectPlayerTempo.PositionOnGrid.y);

            if (_finalPath.Count == 0)
            {
                _finalPath = _currentPath;
                _human = joueur.GetComponent<Human>();
                _gridObjectPlayer = joueur.GetComponent<GridObject>();
            }

            if (_currentPath.Count < _finalPath.Count)
            {
                _finalPath = _currentPath;
                _human = joueur.GetComponent<Human>();
                _gridObjectPlayer = joueur.GetComponent<GridObject>();
            }
        }

        _path = _finalPath;
        _path.RemoveAt(_path.Count - 1);

        if (_path == null || _path.Count == 0)
        {
            return;
        }

        Move(_path);
    }

    private void Update()
    {
        if (_pathWorldPositions == null || _pathWorldPositions.Count == 0)
        {
            return;
        }

        if (_pathWorldPositions.Count > 0 && CanMove)
        {
            TravellingMonster();
        }
    }

    public void AddPM()
    {
        _monsters.MonsterPM = 3;
    }
}
