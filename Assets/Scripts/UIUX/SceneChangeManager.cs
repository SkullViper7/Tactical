using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerManager : MonoBehaviour
{
    [SerializeField] Animator _blackAnim;
    [SerializeField] Animator _musicAnim;


    private void Start()
    {
        _blackAnim.Play("FadeOut");
    }

    public void Click()
    {
        _blackAnim.Play("FadeIn");
        _musicAnim.Play("FadeOut");
        Invoke("SceneChange", 1);
    }

    void SceneChange()
    {
        SceneManager.LoadScene("Final Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
