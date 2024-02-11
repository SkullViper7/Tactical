using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerManager : MonoBehaviour
{
    [SerializeField] private GridObject _actualCharacter;
    [SerializeField] private Grid _targetGrid;
    public Human Hmn;

    //public void SkillRange(int Index)
    //{
    //    Human hmn = _actualCharacter.GetComponent<Human>();
    //    int atkRange = hmn.Skills[Index].Range;

    //    List<Vector2Int> atkPosition = new List<Vector2Int>();

    //    for (int x = -atkRange; x <= atkRange; x++)
    //    {
    //        for (int y = -atkRange; x <= atkRange; y++)
    //        {
    //            if (_targetGrid.CheckBoundary(x, y) == true)
    //            {
    //                atkPosition.Add(
    //                    new Vector2Int(
    //                        _actualCharacter.PositionOnGrid.x + x,
    //                        _actualCharacter.PositionOnGrid.y + y));
    //            }
    //        }
    //    }
    //}

    public void Attack(int Index)
    {
        int pwr = BattleManager.Instance._selectedPlayerSkill._skPwr;
    }
}
