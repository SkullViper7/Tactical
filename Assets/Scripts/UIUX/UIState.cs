using TMPro;
using UnityEngine;

public class UIState : MonoBehaviour
{
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private TextMeshProUGUI _phaseText;
    [SerializeField] private GameObject _btnEndPhase;
    [SerializeField] private Grid _grid;

    // Singleton
    private static UIState _instance = null;

    private UIState() { }

    public static UIState Instance => _instance;
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

    public void DisPlaySelectPhase()
    {
        _playerUI.SetActive(true);
        _phaseText.text = "Select Player";
        _grid.CheckPassableTerrain();
    }

    public void DisplayMovePhase()
    {
        _btnEndPhase.SetActive(true);
        _phaseText.text = "Move Phase";
        _grid.ObstacleLayer = LayerMask.GetMask("Obstacle", "Ennemy", "Player");
        _grid.CheckPassableTerrain();
    }

    public void DisplayAttackPhase()
    {
        _phaseText.text = "Attack Phase";
        _grid.ObstacleLayer = LayerMask.GetMask("Obstacle");
        _grid.CheckPassableTerrain();
    }

    public void DisplayMonsterPhase()
    {
        _playerUI.SetActive(false);
        _btnEndPhase.SetActive(false);
        _phaseText.text = "Monster Phase";
        _grid.ObstacleLayer = LayerMask.GetMask("Obstacle", "Ennemy");
        _grid.CheckPassableTerrain();
    }
}
