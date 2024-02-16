using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public void OpenCanvas(GameObject _canvasToOpen)
    {
        _canvasToOpen.SetActive(true);
    }

    public void CloseCanvas(GameObject _canvasToClose)
    {
        _canvasToClose.SetActive(false);
    }
}
