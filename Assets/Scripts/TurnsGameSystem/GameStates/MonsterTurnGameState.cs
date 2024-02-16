using Cysharp.Threading.Tasks;

public class MonsterTurnGameState : BaseGameState
{
    TurnGameSystemController _turn;
    bool playerHasLost = false;

    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        _turn = turnGameSystem;
        UIState.Instance.DisplayMonsterPhase();
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
            }
        }
    }

    public async void MonstersArePlaying()
    {
        for (int i = 0; i < MonstersManager.Instance.ListMonster.Count; i++)
        {
            BattleManager.Instance.UpdateGrid();

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
                }
            }

            // Checks whether all monsters have played or not
            if (monstersWhoHavePlayed >= MonstersManager.Instance.ListMonster.Count)
            {
                if (PlayerManager.Instance.AllHmn.Count <= 0)
                {
                    playerHasLost = true;
                    _turn.SwitchState(_turn.PlLostGmState);
                }
                else
                {
                    _turn.SwitchState(_turn.SelectPlState);
                }
            }

            MonstersManager.Instance.CurrentMonsterMain.MonstersMovements.TurnFinishedEvent -= Notify;
        }
    }
}
