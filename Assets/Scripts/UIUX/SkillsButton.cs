using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour
{
    public Button buttonComponent;
    public SkillInfo skillInfo;

    public void Setup(SkillInfo skill)
    {
        skillInfo = skill;
        TextMeshProUGUI btnText = GetComponentInChildren<TextMeshProUGUI>();
        btnText.text = skill.SkName;
    }

    public void PrepareSkillUsage()
    {
        PlayerManager.Instance.UseSkillInfo = skillInfo;
        if (skillInfo.SkisHealing)
            BattleManager.Instance.PrepareHeal(skillInfo);
        else
            BattleManager.Instance.PrepareAttack(skillInfo);
        Debug.Log(PlayerManager.Instance.UseSkillInfo.SkRange);
    }
}
