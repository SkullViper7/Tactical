using UnityEngine;

public class Monsters : MonoBehaviour
{
    public MonstersScriptable MS;

    public Sprite MonsterSprite;
    public string MonsterName;
    public int MonsterPVMax;
    public int MonsterPV;
    public int MonsterAttack;
    public int MonsterDefence;
    public int MonsterPM;
    public int MonsterPA;


    private void Awake()
    {
        MonsterSprite = MS.MonsterSprite;
        MonsterName = MS.MonsterName;
        MonsterPVMax = MS.MonsterPVMax;
        MonsterPV = MonsterPVMax;
        MonsterAttack = MS.MonsterAttack;
        MonsterDefence = MS.MonsterDefence;
        MonsterPM = MS.MonsterPM;
        MonsterPA = MS.MonsterPA;
    }
}
