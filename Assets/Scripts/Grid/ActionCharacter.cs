using UnityEngine;
using UnityEngine.InputSystem;

public class ActionCharacter : MonoBehaviour
{
    [SerializeField]
    GridObject _targetCharacter;

    PlayerInput _playerInput;

    HighlightPath _highlightPath;

    /// <summary>
    /// Registering the OnClick function to the input system.
    /// </summary>
    private void Start()
    {
        _highlightPath = GetComponent<HighlightPath>();

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += OnClick;
    }

    /// <summary>
    /// Calling the move function of the target character.
    /// </summary>
    /// <param name="context"></param>
    public void OnClick(InputAction.CallbackContext context)
    {
        // Check if the context for the input started
        if (context.started)
        {
            // If the highlight path component indicates that movement is possible
            if (_highlightPath.CanMove)
            {
                // Move the target character using the PlayerMovement component and the highlighted path
                _targetCharacter.GetComponent<PlayerMovement>().Move(_highlightPath.Path);
                // Disable the highlights after the movement
                _highlightPath.DisableHighights();
            }
        }
    }
}
