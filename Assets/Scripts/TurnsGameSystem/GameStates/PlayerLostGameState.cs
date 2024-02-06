using UnityEngine;
public class PlayerLostGameState : BaseGameState
{

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        // Annoncer Que Le Joueur A Perdu
        Debug.Log("PlayerLostGameState Enters State");
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {

    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {

    }
}
