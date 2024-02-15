public class PlayerMoveState : BaseGameState
{
    TurnGameSystemController _turn;

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        PlayerManager.Instance.CanMoveEvent += Notify;
        PlayerManager.Instance.SetCanMove(true);

        PlayerManager.Instance.CanFindPath = true;
        PlayerManager.Instance.IsMovingState = true;
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        PlayerManager.Instance.CanFindPath = false;
        PlayerManager.Instance.IsMovingState = false;
        PlayerManager.Instance.CanMoveEvent -= Notify;

    }

    public void Notify(bool canMove)
    {
        if (!canMove) {
            _turn.SwitchState(_turn.PlAtkState);
        }
    }
}
