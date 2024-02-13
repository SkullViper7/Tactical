using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Monsters _monsters;
    private MonstersMovements _monstersMovements;
    private Human _human;

    [SerializeField]
    private int _attackCost = 2;

    void Start()
    {
        _monsters = GetComponent<Monsters>();
        _monstersMovements = GetComponent<MonstersMovements>();
    }

    public void UseAttack(Monsters user, Human target)
    {
        // Make sure we have valid arguments
        if (user == null || target == null)
        {
            Debug.LogError("Invalid arguments for UseSkill. Make sure to provide a valid user and target");

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

            // Display the damage or other effects here
            Debug.Log(user.name + " deals " + damage + " damage to " + target.name);
        }
        else
        {
            // If not enough AP, log it
            Debug.Log("Not enough AP to use the skill.");
        }
    }

    public void ResetPA()
    {
        _monsters.MonsterPA = _monsters.MS.MonsterPA;
    }
}
