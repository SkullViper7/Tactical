using UnityEngine;
using UnityEngine.InputSystem;

public class ActionBattle : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Human _targetHumn;
    private Monsters _targetMstr;

    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _ennemiLayer;

    /// <summary>
    /// Registering the OnClick function to the input system.
    /// </summary>
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += OnClick;
    }

    /// <summary>
    /// Calling the skill function of the button skill.
    /// </summary>
    /// <param name="context"></param>
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started && PlayerManager.Instance.WillHeal)
        {
            // Create a ray from the main camera to the current mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Cast a ray from the camera to find the objects hit by the ray within a maximum distance and with a the Terrain layer mask
            if (Physics.Raycast(ray, out hit, float.MaxValue, _playerLayer))
            {
                _targetHumn = hit.collider.gameObject.GetComponentInParent<Human>();
                BattleManager.Instance.PerformHealOnTarget(_targetHumn);
                PlayerManager.Instance.WillHeal = false;
            }
        }

        if (context.started && PlayerManager.Instance.WillDamage)
        {
            // Create a ray from the main camera to the current mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Cast a ray from the camera to find the objects hit by the ray within a maximum distance and with a the Terrain layer mask
            if (Physics.Raycast(ray, out hit, float.MaxValue, _ennemiLayer))
            {
                Debug.Log("Click on Monster");
                _targetMstr = hit.collider.gameObject.GetComponentInParent<Monsters>();
                BattleManager.Instance.PerformDamageOnTarget(_targetMstr);
                PlayerManager.Instance.WillDamage = false;
            }
        }
    }
}
