using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool IsMoving;

    public Human HmnPlay;

    // Singleton
    private static PlayerManager _instance = null;

    private PlayerManager() { }

    public static PlayerManager Instance => _instance;
    //

    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        //
    }
}
