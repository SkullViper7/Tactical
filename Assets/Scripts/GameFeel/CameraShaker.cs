using Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Singleton
    private static CameraShaker _instance = null;

    public static CameraShaker Instance => _instance;

    CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        Invoke("Shake", 1);
    }

    /// <summary>
    /// Shakes the camera.
    /// Type : 0 = Uniform, 1 = Dissipating, 2 = Propagating, 3 = Legacy.
    /// Shape : 0 = Custom, 1 = Recoil, 2 = Bump, 3 = Explosion, 4 = Rumble.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="shape"></param>
    /// <param name="velocity"></param>
    /// <param name="duration"></param>
    public void Shake(int type, int shape, Vector3 velocity, float duration)
    {
        // Set the impulse type based on the 'type' parameter using a switch statement
        switch (type)
        {
            case 0:
                _impulseSource.m_ImpulseDefinition.m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Uniform;
                break;
            case 1:
                _impulseSource.m_ImpulseDefinition.m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Dissipating;
                break;
            case 2:
                _impulseSource.m_ImpulseDefinition.m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Propagating;
                break;
            case 3:
                _impulseSource.m_ImpulseDefinition.m_ImpulseType = CinemachineImpulseDefinition.ImpulseTypes.Legacy;
                break;
        }

        // Set the impulse shape based on the 'shape' parameter using a switch statement
        switch (shape)
        {
            case 0:
                _impulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Custom;
                break;
            case 1:
                _impulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Recoil;
                break;
            case 2:
                _impulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Bump;
                break;
            case 3:
                _impulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Explosion;
                break;
            case 4:
                _impulseSource.m_ImpulseDefinition.m_ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Rumble;
                break;
        }

        // Set the default velocity, impulse duration, and generate the impulse
        _impulseSource.m_DefaultVelocity = velocity;
        _impulseSource.m_ImpulseDefinition.m_ImpulseDuration = duration;
        _impulseSource.GenerateImpulse();
    }
}
