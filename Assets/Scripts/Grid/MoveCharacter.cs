using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField]
    public GridObject TargetCharacter;

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
            TargetCharacter.GetComponent<PlayerMovement>().Move(_highlightPath.Path);
            _highlightPath.DisableHighights();
        }
    }
}
