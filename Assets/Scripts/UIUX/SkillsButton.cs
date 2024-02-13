using UnityEngine;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour
{
    public Button buttonComponent;
    public SkillInfo skillInfo;

    public void Setup(SkillInfo skill)
    {
        skillInfo = skill;
    }
    private void PrepareSkillUsage()
    {
        if (skillInfo.SkisHealing)
            BattleManager.Instance.PrepareHeal(skillInfo);
        else
            BattleManager.Instance.PrepareAttack(skillInfo);
    }
}
