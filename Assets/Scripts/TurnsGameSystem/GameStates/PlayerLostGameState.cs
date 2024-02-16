using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerLostGameState : BaseGameState
{
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {
    }

    public async void GoToLoseScene()
    {
        UIManager.Instance.FadeIn();
        AnimationManager.Instance.FadeOut();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        SceneManager.LoadScene("GameLost");
    }
}
