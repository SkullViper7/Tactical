using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _positionY;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }

    private void Update()
    {
        transform.position = new Vector3(_target.position.x, _positionY, _target.position.z);
    }
}
