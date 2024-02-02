using UnityEngine;

public class TurnGameSystemController : MonoBehaviour
{
    BaseGameState _currentGameState;

    [Header("States")]
    StartGameState _startGameState;
    PlayerTurnGameState _playerTurnGameState;
    MonsterTurnGameState _monsterTurnGameState;
    PlayerLostGameState _playerLostGameState;
    PlayerWonGameState _playerWonGameState;


    public void Start()
    {
        _currentGameState = _startGameState;
        _currentGameState.EnterState(this);
    }

    public void Update()
    {
        _currentGameState.UpdateState(this);
    }

    public void SwitchState(BaseGameState state)
    {
        _currentGameState.ExitState(this);

        _currentGameState = state;
        _currentGameState.EnterState(this);
    }
}
