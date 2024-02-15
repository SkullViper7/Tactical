using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MonsterTurnGameState : BaseGameState
{
    TurnGameSystemController _turn;
    bool playerHasLost = false;

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        for (int i = 0; i < MonstersManager.Instance.ListMonster.Count; i++)
        {
            if (!MonstersManager.Instance.ListMonster[i].Monsters.IsDead)
            {
                MonstersManager.Instance.ListMonster[i].Monsters.ResetPAandPM();
                MonstersManager.Instance.ListMonster[i].MonstersMovements.CanAttack = true;
            }
        }

        MonstersArePlaying();
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        if (!playerHasLost)
        {
            for (int i = 0; i < PlayerManager.Instance.AllHmn.Count; i++)
            {
                PlayerManager.Instance.AllHmn[i].NewTurnResetAction();
                Debug.Log(PlayerManager.Instance.AllHmn[i].CurrentMP);
            }
        }
    }

    public async void MonstersArePlaying()
    {
        for (int i = 0; i < MonstersManager.Instance.ListMonster.Count; i++)
        {
            if (!MonstersManager.Instance.ListMonster[i].Monsters.IsDead)
            {
                MonstersManager.Instance.ListMonster[i].MonstersMovements.SearchPlayerNearby();
                MonstersManager.Instance.CurrentMonsterMain.MonstersMovements.TurnFinishedEvent += Notify;

                await UniTask.WaitUntil(() => MonstersManager.Instance.CurrentMonsterMain.MonstersMovements.TurnFinish == true);
            }
        }
    }

    public void Notify(bool isTurnFinished) {
        if (isTurnFinished)
        {
            // Counts number of monsters who have played
            int monstersWhoHavePlayed = 0;
            for (int i = 0; i < MonstersManager.Instance.ListMonster.Count; i++)
            {
                if (MonstersManager.Instance.ListMonster[i].MonstersMovements.TurnFinish ||
                    MonstersManager.Instance.ListMonster[i].Monsters.IsDead) {

                    monstersWhoHavePlayed++;
                    Debug.Log($"{monstersWhoHavePlayed}-> Après ++");
                }
            }

            // Checks whether all monsters have played or not
            if (monstersWhoHavePlayed >= MonstersManager.Instance.ListMonster.Count)
            {
                int playerWhoAreDead = 0;
                for (int i = 0; i < PlayerManager.Instance.AllHmn.Count; i++)
                {
                    // Checks whether a player is still alive or not
                    if (!PlayerManager.Instance.AllHmn[i].IsDead)
                    {
                        _turn.SwitchState(_turn.SelectPlState);
                        break;
                    }
                    else
                    {
                        playerWhoAreDead++;
                    }
                }

                // Checks whether all players are still alive or not, if it's the case, announce that the player have lost
                if (playerWhoAreDead >= PlayerManager.Instance.AllHmn.Count)
                {
                    playerHasLost = true;
                    _turn.SwitchState(_turn.PlLostGmState);
                }
            }

            MonstersManager.Instance.CurrentMonsterMain.MonstersMovements.TurnFinishedEvent -= Notify;
        }
    }
}
