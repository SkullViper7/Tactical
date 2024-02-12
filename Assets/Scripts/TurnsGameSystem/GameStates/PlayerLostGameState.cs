using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLostGameState : BaseGameState
{
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        SceneManager.LoadScene("GameWon");
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {

    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {

    }
}
