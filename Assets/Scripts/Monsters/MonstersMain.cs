using UnityEngine;

public class MonstersMain : MonoBehaviour
{
    public Monsters Monsters;
    public MonsterAttack MonsterAttack;
    public MonstersMovements MonstersMovements;

    private void Start()
    {
        Monsters = GetComponent<Monsters>();
        MonsterAttack = GetComponent<MonsterAttack>();
        MonstersMovements = GetComponent<MonstersMovements>();
    }
}
