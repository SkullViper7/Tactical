using System.Collections.Generic;
using UnityEngine;

public class SkillsAction : MonoBehaviour

{

    Human Hmn;

    public List<SkillInfo> SkillInfos = new List<SkillInfo>();

    private void Awake()
    {
        Hmn = GetComponent<Human>();
        LoadSkills();
    }

    private void LoadSkills()
    {
        SkillInfos = new List<SkillInfo>();
        foreach (var skill in Hmn.Skills)
        {
            SkillInfos.Add(new SkillInfo(skill));
        }
    }
}
