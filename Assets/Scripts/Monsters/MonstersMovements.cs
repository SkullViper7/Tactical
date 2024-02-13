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
    public bool TurnFinish = false;


    private void Awake()
    {
        _gridObjectMonster = GetComponent<GridObject>();
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
        _monsters = GetComponent<Monsters>();
        _monsterAttack = GetComponent<MonsterAttack>();
    }

    /// <summary>
    /// Converts a list of path nodes to a list of world positions.
    /// </summary>
    /// <param name="path"></param>
    void Move(List<PathNode> path)
    {
        CanMove = true;

        _pathWorldPositions = _gridObjectMonster.TargetGrid.ConvertPathNodesToWorldPositions(path);

        _gridObjectMonster.PositionOnGrid.x = path[path.Count - 1].Pos_X;
        _gridObjectMonster.PositionOnGrid.y = path[path.Count - 1].Pos_Y;

        TravellingMonster();
    }

    /// <summary>
    /// Move the monster and  allows it to attack in close combat.
    /// </summary>
    void TravellingMonster()
    {
        if (_monsters.MonsterPM > 0 && CanMove)
        {
            if (_pathWorldPositions.Count == 0)
            {
                CanMove = false;
                TurnFinish = true;
                Debug.LogError("list of _pathWorldPosition is void");
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
                TurnFinish = true;
            }

        }
        else
        {
            Debug.LogError("The monster lacks MP or is not allowed to move");
            CanMove = false;
            return;
        }
    }

    /// <summary>
    /// find the player closest to the monster.
    /// </summary>
    public void SearchPlayerNearby()
    {
        GameObject[] joueurs = GameObject.FindGameObjectsWithTag("Player");
        _path = new List<PathNode>();
        List<PathNode> _currentPath = new List<PathNode>();

        foreach (GameObject joueur in joueurs)
        {
            _currentPath.Clear();

            GridObject _gridObjectPlayerTempo = joueur.GetComponent<GridObject>();
            _currentPath = _pathFindingScript.FindPath(_gridObjectMonster.PositionOnGrid.x, _gridObjectMonster.PositionOnGrid.y, _gridObjectPlayerTempo.PositionOnGrid.x, _gridObjectPlayerTempo.PositionOnGrid.y);

            if (_path.Count == 0)
            {
                _path = _currentPath;
                _human = joueur.GetComponent<Human>();
                _gridObjectPlayer = joueur.GetComponent<GridObject>();
            }

            if (_currentPath.Count < _path.Count)
            {
                _path = _currentPath;
                _human = joueur.GetComponent<Human>();
                _gridObjectPlayer = joueur.GetComponent<GridObject>();
            }
        }

        if (_path == null || _path.Count == 0)
        {
            Debug.LogError("list of _path is void or null");
            return;
        }

        _path.RemoveAt(_path.Count - 1);

        if (_path == null || _path.Count == 0)
        {
            Debug.LogError("list of _path is void or null");
            return;
        }

        Move(_path);
    }

    /// <summary>
    /// Use this method to move the monster.
    /// </summary>
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

    /// <summary>
    /// Resets PM to the monster.
    /// </summary>
    public void ResetPM()
    {
        _monsters.MonsterPM = _monsters.MS.MonsterPM;
    }
}
