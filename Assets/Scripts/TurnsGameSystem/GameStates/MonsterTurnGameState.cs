using UnityEngine;
public class MonsterTurnGameState : BaseGameState
{
    TurnGameSystemController _turn;
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("MonsterTurnGameState Enters State");
        _turn = turnGameSystem;
        turnGameSystem.GameSystemTransmitter.MonstersCanPlay = true;
        turnGameSystem.GameSystemTransmitter.HaveMonstersPlayed += Notify;
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("MonsterTurnGameState Leaves State");
        turnGameSystem.GameSystemTransmitter.MonstersCanPlay = false;
        turnGameSystem.GameSystemTransmitter.HaveMonstersPlayed -= Notify;
        turnGameSystem.GameSystemTransmitter.MonstersHasPlayedOrNot = false;
    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {
    }

    public void Notify(bool monsterPlayed)
    {
        if (_turn.GameSystemTransmitter.PlayerStillAlive)
        {
            Debug.Log("Switch State to Player Turn");
            _turn.SwitchState(_turn.PlayerTurnGameState);
        }
        else
        {
            Debug.Log("Switch State to Player Lost");
            _turn.SwitchState(_turn.PlayerLostGameState);
        }
    }
}
