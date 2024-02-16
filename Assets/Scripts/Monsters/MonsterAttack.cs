using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private int _attackCost = 2;

    public void UseAttack(Monsters user, Human target)
    {
        // Make sure we have valid arguments
        if (user == null || target == null)
        {
            Debug.Log("Invalid arguments for UseSkill. Make sure to provide a valid user and target");

            return;
        }

        // Check if the user has enough AP to use the skill
        if (user.MonsterPA > 0)
        {
            // Deduct the skill usage cost
            user.MonsterPA -= _attackCost;

            // Calculate damage inflicted
            int damage = user.MonsterAttack - target.Def;

            // Apply damage to target
            target.CurrentHP -= damage;

            StartCoroutine(AnimationManager.Instance.SetTrigger(user.GetComponentInChildren<Animator>(), "Attack", 0));
            StartCoroutine(AnimationManager.Instance.SetTrigger(target.GetComponentInChildren<Animator>(), "Hurt", 1.22f));
            StartCoroutine(CameraShaker.Instance.Shake(0, 3, new Vector3(0, -0.25f, 0), 0.5f, 1.22f));

            // Apply death if the target have 0HP
            if (target.CurrentHP <= 0)
            {
                StartCoroutine(AnimationManager.Instance.SetTrigger(target.GetComponentInChildren<Animator>(), "Death", 0));
                target.IsDead = true;
                PlayerManager.Instance.AllHmn.Remove(target);
            }

            // Display the damage or other effects here
            Debug.Log(user.name + " deals " + damage + " damage to " + target.name);
        }
        else
        {
            // If not enough AP, log it
            Debug.Log("Not enough AP to use the skill.");
        }
    }
}
