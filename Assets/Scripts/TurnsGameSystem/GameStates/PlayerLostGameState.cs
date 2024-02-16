using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerLostGameState : BaseGameState
{
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        GoToLoseScene();
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
    }

    public async void GoToLoseScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(4), ignoreTimeScale: false);
        SceneManager.LoadScene("GameLost");
    }
}
