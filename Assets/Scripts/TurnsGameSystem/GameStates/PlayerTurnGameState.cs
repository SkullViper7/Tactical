
using UnityEngine;
public class PlayerTurnGameState : BaseGameState
{
    TurnGameSystemController _turn;

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("PlayerTurnGameState Enters State");
        _turn = turnGameSystem;
        turnGameSystem.GameSystemTransmitter.PlayerCanPlay = true;
        turnGameSystem.GameSystemTransmitter.HasPlayerHavePlayed += Notify;
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("PlayerTurnGameState Leaves State");
        turnGameSystem.GameSystemTransmitter.PlayerCanPlay = false;
        turnGameSystem.GameSystemTransmitter.HasPlayerHavePlayed -= Notify;
        turnGameSystem.GameSystemTransmitter.PlayerHasPlayedOrNot = false;
    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {
    }

    public void Notify(bool playerPlayed)
    {
        if (_turn.GameSystemTransmitter.AreMonstersStillAlives)
        {
            Debug.Log("Switch State to Monster Turn");
            _turn.SwitchState(_turn.MonsterTurnGameState);
        }
        else
        {
            Debug.Log("Switch State to Player Won");
            _turn.SwitchState(_turn.PlayerWonGameState);
        }
    }
}
