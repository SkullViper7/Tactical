﻿using UnityEngine;

public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.CreateSkillButtons();
    }
}
