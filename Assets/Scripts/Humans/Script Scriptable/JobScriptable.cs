using UnityEngine;

[CreateAssetMenu(fileName = "Job_Name", menuName = "Player/New_Job")]
public class JobScriptable : ScriptableObject
{
    /// <summary>
    /// Health Point Stat.
    /// </summary>
    public int HP;

    /// <summary>
    /// Attak stat.
    /// </summary>
    public int Atk;

    /// <summary>
    /// Defense stat.
    /// </summary>
    public int Def;

    /// <summary>
    /// Movement Point stat.
    /// </summary>
    public int MP;

    /// <summary>
    /// Action Point Stat.
    /// </summary>
    public int AP;

    /// <summary>
    /// Critic Chance Stat.
    /// </summary>
    public float Crts;

    /// <summary>
    /// List of skills used by this job.
    /// </summary>
    public SkillsScriptable[] Skills;
}
