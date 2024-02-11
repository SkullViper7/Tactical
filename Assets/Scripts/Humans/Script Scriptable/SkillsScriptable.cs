using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Name", menuName = "Player/New_Skill")]
public class SkillsScriptable : ScriptableObject
{
    /// <summary>
    /// Name for the Skill
    /// </summary>
    public string Name;

    /// <summary>
    /// Range for the Skill.
    /// </summary>
    public int Range;

    /// <summary>
    /// Power Stat.
    /// </summary>
    public int Pwr;

    /// <summary>
    /// Action Point Cost.
    /// </summary>
    public int SkAp;

    /// <summary>
    /// Is this skills was a support.
    /// </summary>
    public bool IsSupport;
}
