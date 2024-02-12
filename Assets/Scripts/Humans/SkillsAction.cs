using UnityEngine;

public class SkillsAction : MonoBehaviour
{
    Human Hmn;
    public string SkName;
    public int _skRange;
    public int SkPwr;
    public int SkAP;
    public bool SkisHealing;


    private void Start()
    {
        Hmn = GetComponent<Human>();
        FirstSkill();
    }

    public void FirstSkill()
    {
        if (Hmn.Skills[0] != null)
        {
            SkName = Hmn.Skills[0].Name;
            _skRange = Hmn.Skills[0].Range;
            SkPwr = Hmn.Skills[0].Pwr;
            SkAP = Hmn.Skills[0].SkAp;
            SkisHealing = Hmn.Skills[0].IsSupport;
        }
    }

    public void SecondSkill()
    {
        if (Hmn.Skills[1] != null)
        {
            SkName = Hmn.Skills[1].Name;
            _skRange = Hmn.Skills[1].Range;
            SkPwr = Hmn.Skills[1].Pwr;
            SkAP = Hmn.Skills[1].SkAp;
            SkisHealing = Hmn.Skills[1].IsSupport;
        }
    }
}
