using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] Slider PlayerHealth;
    [SerializeField] TextMeshProUGUI PlayerMP;
    [SerializeField] TextMeshProUGUI PlayerAP;

    [SerializeField] Button SkillOne;
    [SerializeField] Button SkillTwo;
    [SerializeField] Button SkillThree;
    [SerializeField] Button SkillFour;


    private static UIManager _instance = null;

    public static UIManager Instance => _instance;

    public void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            _instance = this;
        }
    }

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

    public void SetSkills(SkillsAction skills)
    {
        SkillOne.onClick.AddListener(skills.FirstSkill);
    }
}
