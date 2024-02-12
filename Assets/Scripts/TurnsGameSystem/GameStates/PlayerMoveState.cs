public class PlayerMoveState : BaseGameState
{
    TurnGameSystemController _turn;
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        PlayerManager.Instance.CanMove = true;
        PlayerManager.Instance.CanMoveEvent += Notify;

        PlayerManager.Instance.CanFindPath = true;
    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        PlayerManager.Instance.CanFindPath = false;
        PlayerManager.Instance.CanMoveEvent -= Notify;
    }

    public void Notify(bool canMove)
    {
        if (!canMove) {
            _turn.SwitchState(_turn.PlayerAttackState);
        }
    }
}
