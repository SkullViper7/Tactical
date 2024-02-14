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

    // In a component that manages your user interface.

    // Assign this prefab in the editor.
    public GameObject skillButtonPrefab;

    // The parent of the buttons in your UI hierarchy.
    public Transform buttonsParent;


    private static UIManager _instance = null;

    public static UIManager Instance => _instance;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
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

    public void CreateSkillButtons()
    {
        Human user = PlayerManager.Instance.HmnPlay;

        // Assumer que l'utilisateur a un composant SkillsAction attaché
        SkillsAction skillsAction = user.GetComponent<SkillsAction>();

        // Verifier si skillsAction n'est pas nul
        if (skillsAction == null)
        {
            Debug.LogError("SkillsAction component not found on the user.");
            return;
        }

        // Iterate over each skill in the SkillsAction's SkillInfos list

        foreach (var skillInfo in skillsAction.SkillInfos)
        {
            // Instantiate a new button object from the prefab and set its parent in the UI
            var buttonObj = Instantiate(skillButtonPrefab, buttonsParent);

            // Get the SkillButton component from the instantiated button object
            var skillButton = buttonObj.GetComponent<SkillButton>();

            // Assuming SkillButton's Setup method takes a SkillInfo object
            skillButton.Setup(skillInfo);
        }
    }
}
