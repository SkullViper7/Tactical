using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    GameObject _pauseMenu;

    public void SetPause(bool pause)
    {
        if (pause)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
