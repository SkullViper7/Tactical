public class SkillInfo

{

    public string SkName;

    public int SkRange;

    public int SkPwr;

    public int SkAP;

    public bool SkisHealing;

    public SkillInfo(SkillsScriptable skill)

    {

        SkName = skill.Name;

        SkRange = skill.Range;

        SkPwr = skill.Pwr;

        SkAP = skill.SkAp;

        SkisHealing = skill.IsSupport;

    }

}
