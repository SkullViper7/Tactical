using UnityEngine;
public class MonsterTurnGameState : BaseGameState
{
    TurnGameSystemController _turn;
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        Debug.Log("MonsterTurnGameState Leaves State");
    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {
    }

}
