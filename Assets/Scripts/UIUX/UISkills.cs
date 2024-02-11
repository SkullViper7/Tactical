using UnityEngine;

public class UISkills : MonoBehaviour
{

    // Reference to your Grid

    [SerializeField] private Grid targetGrid;

    [SerializeField] private Human Hmn;

    [SerializeField] private SkillsAction SA;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnSelectButtonPressedSkill()
    {
        // Cast a ray from the position of the mouse to the scene

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Convert the hit position to grid position
            Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);

            // Ensure that the position is indeed on the grid
            if (targetGrid.CheckBoundary(gridPosition))
            {
                // Get the object on the grid at this position
                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);

                // Check if the object is a monster, and if yes, do something
                if (gridObject != null && gridObject.GetComponent<Monsters>() != null)
                {
                    Monsters monster = gridObject.GetComponent<Monsters>();

                    // call the function Use Skill
                    BattleManager.Instance.UseSkill(Hmn, monster, SA);
                }
            }
        }
    }

    private void OnSelectButtonPressedHeal()
    {
        // Cast a ray from the position of the mouse to the scene

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Convert the hit position to grid position
            Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);

            // Ensure that the position is indeed on the grid
            if (targetGrid.CheckBoundary(gridPosition))
            {
                // Get the object on the grid at this position
                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);

                // Check if the object is a monster, and if yes, do something
                if (gridObject != null && gridObject.GetComponent<Human>() != null)
                {
                    Human human = gridObject.GetComponent<Human>();

                    // call the function Use Skill
                    BattleManager.Instance.UseHeal(Hmn, human, SA);
                }
            }
        }
    }
}
