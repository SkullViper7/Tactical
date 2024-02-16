using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TurnGameSystemController : MonoBehaviour
{
    BaseGameState _currentGameState;
    public PlayerMoveState PlMvState { get; private set; }
    public PlayerAttackState PlAtkState { get; private set; }
    public SelectingPlayerState SelectPlState { get; private set; }
    public MonsterTurnGameState MonstTurnGmState { get; private set; }
    public PlayerLostGameState PlLostGmState { get; private set; }
    public PlayerWonGameState PlWonGmState { get; private set; }

    public void Start()
    {
        SelectPlState = new SelectingPlayerState();
        PlMvState = new PlayerMoveState();
        PlAtkState = new PlayerAttackState();

        MonstTurnGmState = new MonsterTurnGameState();

        PlLostGmState = new PlayerLostGameState();
        PlWonGmState = new PlayerWonGameState();

        _currentGameState = new SelectingPlayerState();
        _currentGameState.EnterState(this);
    }

    public void SwitchState(BaseGameState state)
    {
        _currentGameState.ExitState(this);

        _currentGameState = state;
        _currentGameState.EnterState(this);
    }
}
