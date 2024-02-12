using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsMoving;

    public bool CanSelect;
    public bool CanMove;
    public bool CanFight;
    public bool CanFindPath;

    [SerializeField] private Camera _actCam;

    public Human HmnPlay;
    public SkillsAction SAPlayer;
    public GridObject HmnGrid;

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
}
