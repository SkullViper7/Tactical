using UnityEngine;

[CreateAssetMenu(fileName = "Skill_Name", menuName = "Player/New_Skill")]
public class SkillsScriptable : ScriptableObject
{
    /// <summary>
    /// Range for the Skill.
    /// </summary>
    public int Range;

    /// <summary>
    /// Power Stat.
    /// </summary>
    public int Pwr;
}
