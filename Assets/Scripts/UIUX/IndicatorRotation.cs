using UnityEngine;

public class IndicatorRotation : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 90f;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _positionY;

    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(_target.position.x, _positionY, _target.position.z);
    }
}
