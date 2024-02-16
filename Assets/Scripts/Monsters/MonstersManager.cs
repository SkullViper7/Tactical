using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersManager : MonoBehaviour
{
    [SerializeField]
    public List<MonstersMain> ListMonster = new List<MonstersMain>();
    public MonstersMain CurrentMonsterMain;

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

        MonstersMain[] _allGameObjectMonsters = GameObject.FindObjectsOfType<MonstersMain>();

        foreach (MonstersMain _monsters in _allGameObjectMonsters)
        {
            if (_monsters.GetComponent<MonstersMain>() != null)
            {
                ListMonster.Add(_monsters);
            }
        }
    }
}
