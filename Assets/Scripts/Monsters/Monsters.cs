using UnityEngine;

public class Monsters : MonoBehaviour
{
    public MonstersScriptable MS;

    public Sprite MonsterSprite;
    public string MonsterName;
    public int MonsterPV;
    public int MonsterAttack;
    public int MonsterDefence;

    private void Awake()
    {
        MonsterSprite = MS.MonsterSprite;
        MonsterName = MS.MonsterName;
        MonsterPV = MS.MonsterPV;
        MonsterAttack = MS.MonsterAttack;
        MonsterDefence = MS.MonsterDefence;
    }
}
