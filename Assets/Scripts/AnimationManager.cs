using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Singleton
    private static AnimationManager _instance = null;

    public static AnimationManager Instance => _instance;

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

    /// <summary>
    /// Update the animation state for the target animator.
    /// 0 for Idle, 1 for Attack (Heal for healer), 2 for Hurt, 3 for Death and 4 for Run animation.
    /// </summary>
    /// <param name="targetAnimator"></param>
    /// <param name="state"></param>
    public void UpdateAnimState(Animator targetAnimator, int state)
    {
        targetAnimator.SetInteger("State", state);
    }
}
