using UnityEngine;
public class PlayerWonGameState : BaseGameState
{

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        // Annoncer Que Le Joueur A Gagné
        Debug.Log("PlayerWonGameState Enters State");
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {

    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {

    }
}
