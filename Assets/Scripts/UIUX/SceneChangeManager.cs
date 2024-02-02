using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerManager : MonoBehaviour
{
    public void SceneChange(string NameOfScene)
    {
        SceneManager.LoadScene(NameOfScene);
    }
}
