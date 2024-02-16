using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerManager : MonoBehaviour
{
    [SerializeField] Animator _blackAnim;


    private void Start()
    {
        _blackAnim.Play("FadeOut");
    }

    public void Click()
    {
        _blackAnim.Play("FadeIn");
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
