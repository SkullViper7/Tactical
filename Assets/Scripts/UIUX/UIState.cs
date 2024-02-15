using TMPro;
using UnityEngine;

public class UIState : MonoBehaviour
{
    [SerializeField] GameObject _playerUI;
    [SerializeField] TextMeshProUGUI _phaseText;


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
        _phaseText.text = "Select Player Phase";
    }

    public void DisplayMovePhase()
    {
        _phaseText.text = "Move Phase";
    }

    public void DisplayAttackPhase()
    {
        _phaseText.text = "Attack Phase";
    }

    public void DisplayMonsterPhase()
    {
        _playerUI.SetActive(false);
        _phaseText.text = "Monster Phase";
    }
}
