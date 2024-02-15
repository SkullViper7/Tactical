using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public SkillInfo CurrentSkill;
    public MonstersManager MonstersManager;
    // Singleton
    private static BattleManager _instance = null;

    private BattleManager() { }

    public static BattleManager Instance => _instance;
    //

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //
    }

    /// <summary>
    /// Function to get the actual offensive skill for battle.
    /// </summary>
    /// <param name="skillInfo">Skill used.</param>
    public void PrepareAttack(SkillInfo skillInfo)
    {
        Human user = PlayerManager.Instance.HmnPlay;
        CurrentSkill = skillInfo;
        PlayerManager.Instance.WillDamage = true;
        PlayerManager.Instance.WillHeal = false;
    }

    /// <summary>
    /// Fuction to gat the actual healing skill for battle.
    /// </summary>
    /// <param name="skillInfo">Heal Used.</param>
    public void PrepareHeal(SkillInfo skillInfo)
    {
        Human user = PlayerManager.Instance.HmnPlay;
        CurrentSkill = skillInfo;
        PlayerManager.Instance.WillHeal = true;
        PlayerManager.Instance.WillDamage = false;
    }

    public void PerformDamageOnTarget(MonstersMain target)

    {
        // Fuction needed after target monster.
        if (CurrentSkill != null && !CurrentSkill.SkisHealing)
        {
            UseSkill(target, CurrentSkill);
        }
    }

    public void PerformHealOnTarget(Human target)

    {

        // Fuction needed after target human.
        if (CurrentSkill != null && CurrentSkill.SkisHealing)

        {
            UseHeal(target, CurrentSkill);
        }
    }

    /// <summary>
    /// Function called to use a skill on a target
    /// </summary>
    /// <param name="target">Selected Monster.</param>
    /// <param name="skill">Selected Skill</param>

    public void UseSkill(MonstersMain target, SkillInfo skill)
    {
        Human user = PlayerManager.Instance.HmnPlay;
        if (user == null || target == null || skill == null)

        {

            Debug.LogError("Invalid arguments for UseSkill.");

            return;

        }

        if (user.CurrentAP >= skill.SkAP)

        {

            user.CurrentAP -= skill.SkAP;

            int damage = CalculateDamage(skill.SkPwr, user.Atk, target.Monsters.MonsterDefence);

            AnimationManager.Instance.SetTrigger(user.GetComponentInChildren<Animator>(), "Attack");
            AnimationManager.Instance.SetTrigger(target.GetComponentInChildren<Animator>(), "Hurt");
            target.Monsters.MonsterPV -= damage;

            if (target.Monsters.MonsterPV <= 0)
            {
                AnimationManager.Instance.SetTrigger(target.GetComponentInChildren<Animator>(), "Death");
                target.Monsters.IsDead = true;
                MonstersManager.ListMonster.Remove(target);
            }

            UIManager.Instance.DestroyButton();
            PlayerManager.Instance.HmnPlay.SetHumanHasPlayed(true);
            Debug.Log(user.name + " deals " + damage + " damage to " + target.Monsters.MonsterName);

        }

        else
        {
            Debug.Log("Not enough AP to use the skill.");
        }
    }

    /// <summary>
    /// Function called to use a heal on a target.
    /// </summary>
    /// <param name="user">Selected Player.</param>
    /// <param name="target">Selected target Player.</param>
    /// <param name="skillAction">Selected Skill</param>
    public void UseHeal(Human target, SkillInfo skill)
    {
        Human user = PlayerManager.Instance.HmnPlay;
        if (user == null || target == null || skill == null)
        {
            Debug.LogError("Invalid arguments for UseHeal.");
            return;
        }

        if (user.CurrentAP >= skill.SkAP)
        {
            user.CurrentAP -= skill.SkAP;
            int heal = CalculateHeal(skill.SkPwr, user.Atk);
            target.HP += heal; // Assuming that increasing HP is correct for a heal.
            Debug.Log(user.name + " heals " + target.gameObject.name + " for " + heal);
            UIManager.Instance.DestroyButton();
            PlayerManager.Instance.HmnPlay.SetHumanHasPlayed(true);
            AnimationManager.Instance.SetTrigger(user.GetComponentInChildren<Animator>(), "Attack");
        }
        else
        {
            Debug.Log("Not enough AP to use the skill.");
        }
    }

    /// <summary>
    /// Method responsible for calculating damage.
    /// </summary>
    /// <param name="skillPower">Get Skill Power.</param>
    /// <param name="attackerAtk">Get Atk stat of the Player.</param>
    /// <param name="targetDef">Get Def stat of the Target.</param>
    /// <returns>Result of the total Damage.</returns>
    private int CalculateDamage(int skillPower, int attackerAtk, int targetDef)
    {
        // Base damage calculation
        int damage = skillPower + attackerAtk - targetDef;

        // Ensure damage is not negative
        return Mathf.Max(0, damage);
    }

    private int CalculateHeal(int skillPower, int attackerAtk)
    {
        // Base heal calculation
        int heal = skillPower + attackerAtk;

        // Ensure heal is not negative
        return Mathf.Max(0, heal);
    }
}
