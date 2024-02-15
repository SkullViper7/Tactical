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

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        PlayerManager.Instance.HmnPlay.HumanHasPlayed -= Notify;
        PlayerManager.Instance.CanFindPath = false;
    }

    public void Notify(bool humanHasPlayed)
    {
        if (humanHasPlayed) {
            // Counts number of humans who have played
            int playerWhoHasPlayed = 0;
            for (int i = 0; i < PlayerManager.Instance.AllHmn.Count; i++)
            {
                if (PlayerManager.Instance.AllHmn[i].HasPlayed || PlayerManager.Instance.AllHmn[i].IsDead)
                {
                    playerWhoHasPlayed++;
                }
            }

            // Checks whether all humans have played or not
            if (playerWhoHasPlayed >= PlayerManager.Instance.AllHmn.Count) {
                int monstersDead = 0;
                for (int i = 0; i < MonstersManager.Instance.ListMonster.Count; i++) {

                    // Checks whether a monster is still alive or not
                    if (!MonstersManager.Instance.ListMonster[i].Monsters.IsDead) {
                        _turn.SwitchState(_turn.MonstTurnGmState);
                        break;
                    }
                    else {
                        monstersDead++;
                    }
                }

                // Checks whether all monsters are still alive or not, if it's the case, announce that the player have won
                if (monstersDead == MonstersManager.Instance.ListMonster.Count) {
                    _turn.SwitchState(_turn.PlWonGmState);
                }
            }
            else {
                _turn.SwitchState(_turn.SelectPlState);
            }
        }

    }
}
