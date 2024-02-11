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

    public bool CanAttack = false;
    public bool CanMove = true;


    private void Awake()
    {
        _gridObjectMonster = GetComponent<GridObject>();
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
        _monsters = GetComponent<Monsters>();
    }

    public void Move(List<PathNode> path)
    {
        _pathWorldPositions = _gridObjectMonster.TargetGrid.ConvertPathNodesToWorldPositions(path);

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
                _monsters.MonsterPM--;
                CanAttack = false;
            }

            Debug.Log(_pathWorldPositions.Count);
            transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);
            FindPath();
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
        float distanceMinimale = Mathf.Infinity;

        foreach (GameObject joueur in joueurs)
        {
            float distance = Vector3.Distance(transform.position, joueur.transform.position);

            if (distance < distanceMinimale)
            {
                distanceMinimale = distance;
                _human = joueur.GetComponent<Human>();
                _gridObjectPlayer = joueur.GetComponent<GridObject>();
            }
        }

        TravellingMonster();
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
