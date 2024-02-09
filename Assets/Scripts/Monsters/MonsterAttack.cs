using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    Monsters _monsters;
    MonstersMovements _monstersMovements;

    void Start()
    {
        _monsters = GetComponent<Monsters>();
        _monstersMovements = GetComponent<MonstersMovements>();
    }

    public void AttackMelee(GameObject _targetPlayer)
    {
        if (_monstersMovements.CanAttack)
        {
            //Lance l'attaque dans le battle manager
        }
    }
}
