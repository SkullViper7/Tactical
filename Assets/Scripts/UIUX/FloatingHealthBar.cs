using System.Collections;
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

    [SerializeField]
    private Image _healthImage;
    [SerializeField]
    private Coroutine _tween;
    [SerializeField]
    private float _tweenDuration;
    [SerializeField]
    private AnimationCurve _animationCurve;

    [SerializeField]
    private Monsters _monsters;

    //public void UpdateHealthBar(float currentValue, float maxValue)
    //{
    //    _slider.value = currentValue / maxValue;
    //}

    private void OnHealthChanged(int newHealth)
    {
        if (_tween != null)
        {
            StopCoroutine(_tween);
        }

        float targetFillAmount = Mathf.InverseLerp(0, _monsters.MonsterPVMax, newHealth);
        _tween = StartCoroutine(TweenHealthBar(targetFillAmount, _tweenDuration));
    }

    private IEnumerator TweenHealthBar(float targetFillAmount, float duration)
    {
        float startFillAmount = _healthImage.fillAmount;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float x = _animationCurve.Evaluate(t);
            _healthImage.fillAmount = Mathf.LerpUnclamped(startFillAmount, targetFillAmount, x);
            yield return null;
        }
    }

    private void Update()
    {
        transform.position = new Vector3(_target.position.x, _positionY, _target.position.z);
    }
}
