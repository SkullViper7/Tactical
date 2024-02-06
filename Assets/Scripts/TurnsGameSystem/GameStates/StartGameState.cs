using UnityEngine;

public class StartGameState : BaseGameState
{

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("StartGameState Enters State");
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("StartGameState Leaves State");
    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {
        if (turnGameSystem.GameSystemTransmitter.GameLaunched)
        {
            turnGameSystem.SwitchState(turnGameSystem.PlayerTurnGameState);
        }
    }
}
