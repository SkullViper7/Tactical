using UnityEngine;

public class MonstersMain : MonoBehaviour
{
    Monsters _monsters;
    MonsterAttack _monsterAttack;
    MonstersMovements _monstersMovements;

    private void Start()
    {
        _monsters = GetComponent<Monsters>();
        _monsterAttack = GetComponent<MonsterAttack>();
        _monstersMovements = GetComponent<MonstersMovements>();
    }
}
