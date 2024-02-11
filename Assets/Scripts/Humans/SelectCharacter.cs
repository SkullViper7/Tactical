using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [field : SerializeField] private GameObject CharacterSelected { get; set; }
    [field: SerializeField] private PlayerStatsDisplayUI _statsDisplayUI { get; set; }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                CharacterSelected = hit.collider.gameObject;

                if (CharacterSelected != null && CharacterSelected.CompareTag("Player"))
                {
                    Debug.Log("Hey, 100/100");
                }
            }
        }
    }
}
