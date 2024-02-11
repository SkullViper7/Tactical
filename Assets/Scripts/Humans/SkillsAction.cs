using UnityEngine;

public class SkillsAction : MonoBehaviour
{
    Human Hmn;
    public string _skName;
    public int _skRange;
    public int _skPwr;
    public bool _skIsHealing;


    private void Start()
    {
        Hmn = GetComponent<Human>();
        FirstSkill();
    }

    public void FirstSkill()
    {
        if (Hmn.Skills[0] != null)
        {
            _skName = Hmn.Skills[0].Name;
            _skRange = Hmn.Skills[0].Range;
            _skPwr = Hmn.Skills[0].Pwr;
            _skIsHealing = Hmn.Skills[0].IsSupport;
        }
    }
}
