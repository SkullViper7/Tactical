using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField]
    private JobScriptable _job;

    public int HP;
    public int Atk;
    public int Def;
    public int MP;
    public int AP;
    public float Crts;

    public SkillsScriptable[] Skills;

    private void Awake()
    {
        HP = _job.HP;
        Atk = _job.Atk;
        Def = _job.Def;
        MP = _job.MP;
        AP = _job.AP;
        Crts = _job.Crts;
        Skills = _job.Skills;
    }
}
