using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerWonGameState : BaseGameState
{
    public override void EnterState(TurnGameSystemController turnGameSystem)
    {
        GoToWinScene();
    }

    public override void ExitState(TurnGameSystemController turnGameSystem)
    {

    }

    public async void GoToWinScene()
    {
        UIManager.Instance.FadeIn();
        AnimationManager.Instance.FadeOut();
        await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        SceneManager.LoadScene("GameWon");

    }
}
