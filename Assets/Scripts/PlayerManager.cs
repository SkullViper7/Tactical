using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsMovingState;

    public bool CanSelect;
    public event Action<bool> CanSelectEvent;

    public bool CanMove;
    public event Action<bool> CanMoveEvent;

    public bool CanFight;
    public bool CanFindPath;

    public bool WillHeal = false;
    public bool WillDamage = false;
    public bool HasAttacked;

    [SerializeField] private Camera _actCam;

    public Human HmnPlay;
    public SkillsAction SAPlayer;
    public GridObject HmnGrid;
    public PlayerMovement HmnMove;
    public SkillInfo UseSkillInfo;

    public List<Human> AllHmn = new List<Human>();

    // Singleton
    private static PlayerManager _instance = null;

    private PlayerManager() { }

    public static PlayerManager Instance => _instance;
    //

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Human[] _allGameObjectHmn = GameObject.FindObjectsOfType<Human>();

        foreach (Human _human in _allGameObjectHmn)
        {
            if (_human.GetComponent<Human>() != null)
            {
                AllHmn.Add(_human);
            }
        }
    }

    public void SetCanSelect(bool canSelected)
    {
        CanSelect = canSelected;
        CanSelectEvent?.Invoke(CanSelect);
    }

    public void SetCanMove(bool canMove)
    {
        CanMove = canMove;
        CanMoveEvent?.Invoke(CanMove);
    }
}
