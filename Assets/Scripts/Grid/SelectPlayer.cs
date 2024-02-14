using UnityEngine;
using UnityEngine.InputSystem;

public class SelectPlayer : MonoBehaviour
{

    [SerializeField]
    Grid _targetGrid;

    [SerializeField] private LayerMask _playerLayer;

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += OnClick;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started && PlayerManager.Instance.CanSelect)
        {
            // Create a ray from the main camera to the current mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Cast a ray from the camera to find the objects hit by the ray within a maximum distance and with a the Terrain layer mask
            if (Physics.Raycast(ray, out hit, float.MaxValue, _playerLayer))
            {
                PlayerManager.Instance.HmnGrid = hit.collider.gameObject.GetComponentInParent<GridObject>();
                PlayerManager.Instance.HmnPlay = hit.collider.gameObject.GetComponentInParent<Human>();
                PlayerManager.Instance.SAPlayer = hit.collider.gameObject.GetComponentInParent<SkillsAction>();
                PlayerManager.Instance.HmnMove = hit.collider.gameObject.GetComponentInParent<PlayerMovement>();
                UIManager.Instance.UpdatePlayerStatsUI(PlayerManager.Instance.HmnPlay);

                PlayerManager.Instance.SetCanSelect(false);
            }
        }
    }
}
