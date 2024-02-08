using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField]
    GridObject _targetCharacter;

    PlayerInput _playerInput;

    HighlightPath _highlightPath;

    private void Start()
    {
        _highlightPath = GetComponent<HighlightPath>();

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += OnClick;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _targetCharacter.GetComponent<PlayerMovement>().Move(_highlightPath.Path);
            _highlightPath.DisableHighights();
        }
    }
}
