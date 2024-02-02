using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Create Monster")]
public class MonstersScriptable : ScriptableObject
{
    public Sprite MonsterSprite;
    public string MonsterName;
    public int MonsterPV;
    public int MonsterAttack;
    public int MonsterDefence;
}
