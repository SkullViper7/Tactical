using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField]
    private JobScriptable _job;

    public int HP { get; private set; }
    public int CurrentHP;
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int MP { get; private set; }
    public int CurrentMP { get; private set; }
    public int AP { get; private set; }
    public int CurrentAP { get; private set; }
    public float Crts { get; private set; }

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
