using System.Collections;
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
    /// 0 for Idle, 1 for Run.
    /// </summary>
    /// <param name="targetAnimator"></param>
    /// <param name="state"></param>
    public IEnumerator UpdateAnimState(Animator targetAnimator, int state, float delay)
    {
        yield return new WaitForSeconds(delay);

        targetAnimator.SetInteger("State", state);
    }

    /// <summary>
    /// Set the trigger for the target animator.
    /// Hurt, Death or Attack.
    /// </summary>
    /// <param name="targetAnimator"></param>
    /// <param name="trigger"></param>
    public IEnumerator SetTrigger(Animator targetAnimator, string trigger, float delay)
    {
        yield return new WaitForSeconds(delay);

        targetAnimator.SetTrigger(trigger);
    }
}
