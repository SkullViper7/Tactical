using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Human _selectedPlayerStats;
    public SkillsAction _selectedPlayerSkill;
    public Monsters _selectedMonster;

    //Singleton
    private static BattleManager _instance = null;

    private BattleManager() { }

    public static BattleManager Instance => _instance;
    //

    private void Awake()
    {
        //Singleton
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
    }

    public void SelectedEntities()
    {
        
    }
}
