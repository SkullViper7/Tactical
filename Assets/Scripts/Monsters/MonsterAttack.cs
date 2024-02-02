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
    }

    public void AttackMelee(GameObject _targetPlayer)
    {
        //_targetPlayer.Player.PV -= _monsters.MonsterAttack;
    }
}
