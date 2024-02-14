using System.Collections;
using UnityEngine;

public class MonsterTurnGameState : BaseGameState
{
    TurnGameSystemController _turn;
    bool playerHasLost = false;

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        for (int i = 0; i < MonstersManager.Instance.ListMonster.Length; i++)
        {
            if (!MonstersManager.Instance.ListMonster[i].Monsters.IsDead)
            {
                MonstersManager.Instance.ListMonster[i].Monsters.ResetPAandPM();
            }
        }

        StartCoroutine(MonstersArePlaying());
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
        if (!playerHasLost)
        {
            for (int i = 0; i < PlayerManager.Instance.AllHmn.Length; i++)
            {
                PlayerManager.Instance.AllHmn[i].NewTurnResetAction();
            }
        }
    }

    public IEnumerator MonstersArePlaying()
    {
        for (int i = 0; i < MonstersManager.Instance.ListMonster.Length; i++)
        {
            if (!MonstersManager.Instance.ListMonster[i].Monsters.IsDead)
            {
                int antiWhile = 0;
                MonstersManager.Instance.ListMonster[i].MonstersMovements.SearchPlayerNearby();
                MonstersManager.Instance.CurrentMonsterMain.MonstersMovements.TurnFinishedEvent += Notify;
                while (!MonstersManager.Instance.ListMonster[i].MonstersMovements.TurnFinish || antiWhile <= 3600) {
                    yield return null;
                    antiWhile++;
                }

                if (antiWhile >= 3600)
                {
                    Debug.Log("Boucle while terminée");
                }

                MonstersManager.Instance.CurrentMonsterMain.MonstersMovements.TurnFinishedEvent -= Notify;
            }
        }
    }

    public void Notify(bool isTurnFinished) {
        if (isTurnFinished)
        {
            // Counts number of monsters who have played
            int monstersWhoHavePlayed = 0;
            for (int i = 0; i < MonstersManager.Instance.ListMonster.Length; i++)
            {
                if (MonstersManager.Instance.ListMonster[i].MonstersMovements.TurnFinish ||
                    MonstersManager.Instance.ListMonster[i].Monsters.IsDead) {

                    monstersWhoHavePlayed++;
                }
            }

            // Checks whether all monsters have played or not
            if (monstersWhoHavePlayed >= MonstersManager.Instance.ListMonster.Length)
            {
                int playerWhoAreDead = 0;
                for (int i = 0; i < PlayerManager.Instance.AllHmn.Length; i++)
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
                if (playerWhoAreDead >= PlayerManager.Instance.AllHmn.Length)
                {
                    playerHasLost = true;
                    _turn.SwitchState(_turn.PlLostGmState);
                }
            }
        }
    }
}
