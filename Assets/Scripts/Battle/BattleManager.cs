using UnityEngine;

public class BattleManager : MonoBehaviour
{
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
    /// Function called to use a skill on a target
    /// </summary>
    /// <param name="user">Selected Player.</param>
    /// <param name="target">Selected Monster.</param>
    /// <param name="skillAction">Selected Skill</param>

    public void UseSkill(Human user, Monsters target, SkillsAction skillAction)
    {
        // Make sure we have valid arguments
        if (user == null || target == null || skillAction == null)
        {
            Debug.LogError("Invalid arguments for UseSkill. Make sure to provide a valid user, target, and skillAction.");

            return;
        }

        // Check if the user has enough AP to use the skill
        if (user.CurrentAP >= skillAction.SkAP)
        {
            // Deduct the skill usage cost
            user.CurrentAP -= skillAction.SkAP;

            // Calculate damage inflicted
            int damage = CalculateDamage(skillAction.SkPwr, user.Atk, target.MonsterDefence);

            // Apply damage to target
            target.MonsterPV -= damage;

            if (target.MonsterPV <= 0)
            {
                target.IsDead = true;
            }

            // Display the damage
            Debug.Log(user.name + " deals " + damage + " damage to " + target.MonsterName);
        }
        else
        {
            // If not enough AP, log it
            Debug.Log("Not enough AP to use the skill.");
        }
    }

    /// <summary>
    /// Function called to use a heal on a target.
    /// </summary>
    /// <param name="user">Selected Player.</param>
    /// <param name="target">Selected target Player.</param>
    /// <param name="skillAction">Selected Skill</param>

    public void UseHeal(Human user, Human target, SkillsAction skillAction)
    {
        // Make sure we have valid arguments
        if (user == null || target == null || skillAction == null)
        {
            Debug.LogError("Invalid arguments for UseSkill. Make sure to provide a valid user, target, and skillAction.");

            return;
        }

        // Check if the user has enough AP to use the skill
        if (user.CurrentAP >= skillAction.SkAP)
        {
            // Deduct the skill usage cost
            user.CurrentAP -= skillAction.SkAP;

            // Calculate heal inflicted
            int heal = CalculateHeal(skillAction.SkPwr, user.Atk);

            // Apply heal to target
            target.HP -= heal;

            // Display the heal or other effects here
            Debug.Log(user.name + " deals " + heal + " heal to " + target.gameObject.name);
        }
        else
        {
            // If not enough AP, log it
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
