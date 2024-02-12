using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    public Slider PlayerHealth;
    public TextMeshProUGUI PlayerMP;
    public TextMeshProUGUI PlayerAP;

    public void UpdatePlayerStatsUI(Human human)
    {
        // Set up Name
        _playerName.text = human.name;

        // Set up HP
        PlayerHealth.maxValue = human.HP;
        PlayerHealth.value = human.CurrentHP;

        // Set up MP
        PlayerMP.text = $"{human.CurrentMP} / {human.MP}";

        // Set up AP
        PlayerAP.text = $"{human.CurrentAP} / {human.CurrentAP}";
    }
}
