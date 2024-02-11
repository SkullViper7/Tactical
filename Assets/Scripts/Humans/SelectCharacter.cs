﻿using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [field : SerializeField] private GameObject CharacterSelected { get; set; }
    [field : SerializeField] private TurnGameSystemTransmitter _gameSystemTransmitter { get; set; }
    [field: SerializeField] private PlayerStatsDisplayUI _statsDisplayUI { get; set; }

    [SerializeField] MoveCharacter _moveCharacterSelected;
    [SerializeField] HighlightPath _highlightPathCharacterSelected;

    public void Update()
    {
        if (_gameSystemTransmitter.PlayerCanPlay)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue))
                {
                    CharacterSelected = hit.collider.gameObject;

                    if (CharacterSelected != null && CharacterSelected.CompareTag("Player"))
                    {
                        Debug.Log("Hey, 100/100");
                        _moveCharacterSelected.TargetCharacter = null;
                    }
                }
            }
        }
    }
}
