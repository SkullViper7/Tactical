using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingPlayerState : BaseGameState
{
    TurnGameSystemController _turn;

    public override void EnterState(TurnGameSystemController turnGameSystem) {
        _turn = turnGameSystem;
        UIState.Instance.DisPlaySelectPhase();

        PlayerManager.Instance.SetCanSelect(true);
        PlayerManager.Instance.CanSelectEvent += Notify;
    }

    public override void ExitState(TurnGameSystemController turnGameSystem) {
        PlayerManager.Instance.CanSelectEvent -= Notify;
    }

    public void Notify(bool canSelect) {
        if (!canSelect && !PlayerManager.Instance.HmnPlay.HasPlayed) {
            _turn.SwitchState(_turn.PlMvState);
        }
        else {
            PlayerManager.Instance.CanSelect = true;
        }
    }
}
