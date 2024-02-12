using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour
{
    PlayerInput _playerInput;

    HighlightPath _highlightPath;

    [SerializeField]
    LayerMask _terrainLayer;

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
            if (PlayerManager.Instance.CanMove)
            {
                // Create a ray from the main camera to the current mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Cast a ray from the camera to find the objects hit by the ray within a maximum distance and with a the Terrain layer mask
                if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
                {
                    // If the highlight path component indicates that movement is possible
                    if (_highlightPath.CanMove)
                    {
                        // Move the target character using the PlayerMovement component and the highlighted path
                        PlayerManager.Instance.HmnGrid.GetComponent<PlayerMovement>().Move(_highlightPath.Path);
                        // Disable the highlights after the movement
                        _highlightPath.DisableHighights();
                    }
                }
            }
        }
    }
}
