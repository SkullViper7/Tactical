using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsMoving;

    [SerializeField] private Camera _actCam;

    public Human HmnPlay;
    public GridObject HmnGrid;

    private MoveCharacter _mvPlayer;
    private HighlightPath _lightPath;
    private SelectPlayer _selectPlayer;

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
        //
    }

    private void Start()
    {
        _mvPlayer = _actCam.GetComponent<MoveCharacter>();
        _lightPath = _actCam.GetComponent<HighlightPath>();
        _selectPlayer = _actCam.GetComponent<SelectPlayer>();

        _mvPlayer.enabled = false;
        _lightPath.enabled = false;
    }

    public void ActivatePlayer()
    {
        _mvPlayer.enabled = true;
        _lightPath.enabled = true;
        _selectPlayer.enabled = false;
    }

    public void DesactivatePlayer()
    {
        _mvPlayer.enabled = false;
        _lightPath.enabled = false;
        _selectPlayer.enabled = true;
    }
}
