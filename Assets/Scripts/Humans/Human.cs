using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField]
    private JobScriptable _job;

    public int HP;
    public int CurrentHP;
    public int Atk;
    public int Def;
    public int MP;
    public int CurrentMP;
    public int AP;
    public int CurrentAP;
    public float Crts;

    public SkillsScriptable[] Skills;

    private void Awake()
    {
        HP = _job.HP;
        CurrentHP = _job.HP;
        Atk = _job.Atk;
        Def = _job.Def;
        MP = _job.MP;
        CurrentMP = _job.MP;
        AP = _job.AP;
        CurrentAP = _job.AP;
        Crts = _job.Crts;
        Skills = _job.Skills;
    }

    /// <summary>
    /// Will reset MP and AP for the player.
    /// </summary>
    public void NewTurnResetAction()
    {
        MP = CurrentMP;
        AP = CurrentAP;
    }
}
