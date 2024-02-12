using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersManager : MonoBehaviour
{
    [SerializeField]
    public MonstersMain[] ListMonster;

    private static MonstersManager _instance = null;

    private MonstersManager() { }

    public static MonstersManager Instance => _instance;
    //

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //

        ListMonster = GameObject.FindObjectsOfType<MonstersMain>();
    }
}
