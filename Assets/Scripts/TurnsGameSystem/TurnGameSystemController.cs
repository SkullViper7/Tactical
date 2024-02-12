using UnityEngine;

public class TurnGameSystemController : MonoBehaviour
{
    BaseGameState _currentGameState;
    public TurnGameSystemTransmitter GameSystemTransmitter;

    public StartGameState StartGameState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }
    public PlayerAttackState PlayerAttackState { get; private set; }
    public SelectingPlayerState SelectingPlayerState { get; private set; }
    public MonsterTurnGameState MonsterTurnGameState { get; private set; }
    public PlayerLostGameState PlayerLostGameState { get; private set; }
    public PlayerWonGameState PlayerWonGameState { get; private set; }

    public void Start()
    {
        StartGameState = new StartGameState();
        MonsterTurnGameState = new MonsterTurnGameState();
        PlayerLostGameState = new PlayerLostGameState();
        PlayerWonGameState = new PlayerWonGameState();

        _currentGameState = StartGameState;
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
