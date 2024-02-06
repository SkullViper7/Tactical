using UnityEngine;

public class TurnGameSystemController : MonoBehaviour
{
    BaseGameState _currentGameState;
    public TurnGameSystemTransmitter GameSystemTransmitter;

    public StartGameState _startGameState { get; private set; }

    public PlayerTurnGameState PlayerTurnGameState { get; private set; }

    public MonsterTurnGameState MonsterTurnGameState { get; private set; }

    public PlayerLostGameState PlayerLostGameState { get; private set; }

    public PlayerWonGameState PlayerWonGameState { get; private set; }

    public void Start()
    {
        _startGameState = new StartGameState();
        PlayerTurnGameState = new PlayerTurnGameState();
        MonsterTurnGameState = new MonsterTurnGameState();
        PlayerLostGameState = new PlayerLostGameState();
        PlayerWonGameState = new PlayerWonGameState();

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
