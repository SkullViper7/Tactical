using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private GameObject _objectToToggle;

    public void ToggleBox()
    {
        if (_objectToToggle.activeSelf)
        {
            _objectToToggle.SetActive(false);
        }
        else
        {
            _objectToToggle.SetActive(true);
        }
    }
}
