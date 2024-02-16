using System;
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

    private List<PathNode> _path = new List<PathNode>();
    private PathFinding _pathFindingScript;

    private MonstersMain _monstersMain;

    public bool CanAttack = false;
    public bool CanMove = true;
    public bool TurnFinish = false;

    [SerializeField] private float turnSpeed = 10f;

    public event Action<bool> TurnFinishedEvent;


    private void Awake()
    {
        _gridObjectMonster = GetComponent<GridObject>();
        _pathFindingScript = _targetGrid.GetComponent<PathFinding>();
        _monstersMain = GetComponent<MonstersMain>();
    }

    public void HasTurnFinished(bool turnFinished)
    {
        TurnFinish = turnFinished;
        TurnFinishedEvent?.Invoke(TurnFinish);
    }

    /// <summary>
    /// Converts a list of path nodes to a list of world positions.
    /// </summary>
    /// <param name="path"></param>
    void Move(List<PathNode> path)
    {
        _targetGrid.CheckPassableTerrain();

        CanMove = true;

        _pathWorldPositions = _gridObjectMonster.TargetGrid.ConvertPathNodesToWorldPositions(path);

        int targetNodeIndex = Mathf.Min(_monstersMain.Monsters.MonsterPM - 1, path.Count - 1);

        _gridObjectMonster.PositionOnGrid.x = path[targetNodeIndex].Pos_X;
        _gridObjectMonster.PositionOnGrid.y = path[targetNodeIndex].Pos_Y;
        TravellingMonster();
    }

    /// <summary>
    /// Move the monster and  allows it to attack in close combat.
    /// </summary>
    void TravellingMonster()
    {
        if (_monstersMain.Monsters.MonsterPM > 0 && CanMove)
        {
            // Calculation of the direction to the next target position.
            Vector3 targetDirection = _pathWorldPositions[0] - transform.position;

            // Check if the direction is not zero (the player is not already at the destination).
            if (targetDirection != Vector3.zero)

            {

                // Calculate the rotation needed to look in the direction of the target.
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                targetRotation *= Quaternion.Euler(0, 90, 0);



                // Interpolate to this rotation to smooth the transition using Quaternion.Lerp or Quaternion.Slerp.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, _pathWorldPositions[0], _moveSpeed * Time.deltaTime);
            StartCoroutine(AnimationManager.Instance.UpdateAnimState(gameObject.GetComponentInChildren<Animator>(), 1, 0));

            if (Vector3.Distance(transform.position, _pathWorldPositions[0]) < 0.05f)
            {
                _pathWorldPositions.RemoveAt(0);
                _monstersMain.Monsters.MonsterPM--;
                StartCoroutine(AnimationManager.Instance.UpdateAnimState(gameObject.GetComponentInChildren<Animator>(), 0, 0));
            }

            if (Vector3.Distance(transform.position, _human.transform.position) < 1.1f && _monstersMain.Monsters.MonsterPA > 0 && CanAttack)
            {
                if (_human == null)
                {
                    HasTurnFinished(true);
                    return;
                }

                _monstersMain.MonsterAttack.UseAttack(_monstersMain.Monsters, _human);
                CanAttack = false;
                HasTurnFinished(true);
            }
        }
        else
        {
            HasTurnFinished(true);
            CanMove = false;
            return;
        }
    }

    /// <summary>
    /// find the closest player to the monster.
    /// </summary>
    public void SearchPlayerNearby()
    {
        _path.Clear();
        MonstersManager.Instance.CurrentMonsterMain = this._monstersMain;

        List<PathNode> _currentPath = new List<PathNode>();

        for (int i = 0; i < PlayerManager.Instance.AllHmn.Count; i++)
        {
            _currentPath.Clear();

            Human tempHuman = PlayerManager.Instance.AllHmn[i];
            GridObject _gridObjectPlayerTempo = PlayerManager.Instance.AllHmn[i].gameObject.GetComponent<GridObject>();
            _currentPath = _pathFindingScript.FindPath(_gridObjectMonster.PositionOnGrid.x, _gridObjectMonster.PositionOnGrid.y, _gridObjectPlayerTempo.PositionOnGrid.x, _gridObjectPlayerTempo.PositionOnGrid.y);

            if (_path.Count == 0)
            {
                _path.AddRange(_currentPath);
                _human = tempHuman;
                _gridObjectPlayer = _gridObjectPlayerTempo;
            }

            if (_currentPath.Count < _path.Count)
            {
                _path.Clear();
                _path.AddRange(_currentPath);
                _human = tempHuman;
                _gridObjectPlayer = _gridObjectPlayerTempo;
            }
        }

        _path.RemoveAt(_path.Count - 1);

        if (_path == null)
        {
            return;
        }

        if (_path.Count == 0)
        {
            _monstersMain.MonsterAttack.UseAttack(_monstersMain.Monsters, _human);
            CanMove = false;
            HasTurnFinished(true);
        }
        else
        {
            Move(_path);
        }
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
}
