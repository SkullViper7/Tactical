using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseGameState
{
    TurnGameSystemController _turn;
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        PlayerManager.Instance.CanFindPath = true;
        PlayerManager.Instance.HmnPlay.HumanHasPlayed += Notify;
    }

    public override void UpdateState(TurnGameSystemController turnGameSystem)
    {
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        PlayerManager.Instance.HmnPlay.HumanHasPlayed -= Notify;
        PlayerManager.Instance.CanFindPath = false;
    }

    public void Notify(bool humanHasPlayed)
    {
        if (humanHasPlayed) {
            int playerWhoHasPlayed = 0;
            for (int i = 0; i < PlayerManager.Instance.AllHmn.Length; i++)
            {
                if (!PlayerManager.Instance.AllHmn[i].HasPlayed)
                {
                    playerWhoHasPlayed++;
                }
            }

            if (playerWhoHasPlayed >= PlayerManager.Instance.AllHmn.Length) {
                int monstersDead = 0;
                for (int i = 0; i < MonstersManager.Instance.ListMonster.Length; i++) {
                    if (!MonstersManager.Instance.ListMonster[i].Monsters.IsDead) {
                        _turn.SwitchState(_turn.MonsterTurnGameState);
                        break;
                    }
                    else {
                        monstersDead++;
                    }
                }

                if (monstersDead == MonstersManager.Instance.ListMonster.Length) {
                    _turn.SwitchState(_turn.PlayerWonGameState);
                }

            }
            else {
                _turn.SwitchState(_turn.SelectingPlayerState);
            }
        }

    }
}
