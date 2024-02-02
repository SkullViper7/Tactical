using UnityEngine;

public abstract class BaseGameState : MonoBehaviour
{

    public abstract void EnterState(TurnGameSystemController turnGameSystem);

    public abstract void ExitState(TurnGameSystemController turnGameSystem);

    public abstract void UpdateState(TurnGameSystemController turnGameSystem);


}
