using UnityEngine;

public class UISkills : MonoBehaviour
{

    // Reference to your Grid

    [SerializeField] private Grid _targetGrid;

    [SerializeField] private Human _hmn;

    [SerializeField] private SkillsAction _sa;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnSelectButtonPressedSkill()
    {
        // Cast a ray from the position of the mouse to the scene

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Convert the hit position to grid position
            Vector2Int gridPosition = _targetGrid.GetGridPosition(hit.point);

            // Ensure that the position is indeed on the grid
            if (_targetGrid.CheckBoundary(gridPosition))
            {
                // Get the object on the grid at this position
                GridObject gridObject = _targetGrid.GetPlacedObject(gridPosition);

                // Check if the object is a monster, and if yes, do something
                if (gridObject != null && gridObject.GetComponent<Monsters>() != null)
                {
                    Monsters monster = gridObject.GetComponent<Monsters>();

                    // call the function Use Skill
                    BattleManager.Instance.UseSkill(PlayerManager.Instance.HmnPlay, monster, _sa);
                }
            }
        }
    }

    private void OnSelectButtonPressedHeal()
    {
        // Cast a ray from the position of the mouse to the scene

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Convert the hit position to grid position
            Vector2Int gridPosition = _targetGrid.GetGridPosition(hit.point);

            // Ensure that the position is indeed on the grid
            if (_targetGrid.CheckBoundary(gridPosition))
            {
                // Get the object on the grid at this position
                GridObject gridObject = _targetGrid.GetPlacedObject(gridPosition);

                // Check if the object is a monster, and if yes, do something
                if (gridObject != null && gridObject.GetComponent<Human>() != null)
                {
                    Human human = gridObject.GetComponent<Human>();

                    // call the function Use Skill
                    BattleManager.Instance.UseHeal(PlayerManager.Instance.HmnPlay, human, _sa);
                }
            }
        }
    }
}
