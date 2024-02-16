using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] Slider PlayerHealth;
    [SerializeField] TextMeshProUGUI PlayerMP;
    [SerializeField] Animator _blackAnim;

    // space between button
    [SerializeField] private float buttonSpacing = 100f;

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

    private void Start()
    {
        _blackAnim.Play("FadeOut");
    }

    public void FadeIn()
    {
        _blackAnim.Play("FadeIn");
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
    }

    public void CreateSkillButtons()
    {
        Human user = PlayerManager.Instance.HmnPlay;
        int index = 0;

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

            RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2(index * buttonSpacing, rectTransform.anchoredPosition.y);

            index++;
        }
    }

    public void DestroyButton()
    {
        for (var i = buttonsParent.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(buttonsParent.transform.GetChild(i).gameObject);
        }
    }

    public void EndCurrentPlayerStateButton()
    {
        if(PlayerManager.Instance.IsMovingState)
        {
            PlayerManager.Instance.SetCanMove(false);
            CreateSkillButtons();
        }
        else
        {
            DestroyButton();
            PlayerManager.Instance.HmnPlay.SetHumanHasPlayed(true);
        }
    }
}
